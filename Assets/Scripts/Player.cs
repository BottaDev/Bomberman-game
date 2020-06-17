using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerNum playerNum;
    public List<Renderer> rendererList = new List<Renderer>();      // Objetos que se pintaran de rojo al recibir daño
    [Header("Player Settings")]
    public int life = 3;
    [Range(min:4, max: 6)]
    public float speed = 4;
    // public int bombStack = 3;       Revisar mecanica de bombas limitadas para el final

    //[Header("Attack Settings")]       Revisar mecanica de ataque mele para el final
    //public float damage = 0.5f;
    //public float attackCd;
    //public float attackRange = 0.2f;

    [Header("Bomb Settings")]
    [Range(min: 0.5f, max: 3f)]
    public float bombCd = 3;
    [Range(min: 0.5f, max: 3f)]
    public float bombTimeToExplode = 3f;
    [Range(min: 1, max: 5)]
    public int bombRange = 2;   // Valor expresado en bloques del mapa
    [HideInInspector]
    public Animator animator;

    private bool canBeDamaged = true;
    private Color normalColor;

    private void Start()
    {
        if (playerNum == PlayerNum.Player1)
        {
            //UIManager.instance.SetPlayer1Bombs(bombStack);
            UIManager.instance.SetPlayer1HP(life);
        }
        else
        {
            //UIManager.instance.SetPlayer2Bombs(bombStack);
            UIManager.instance.SetPlayer2HP(life);
        }

        animator = GetComponent<Animator>();

        normalColor = rendererList[1].material.color;
    }

    public void TakeDamage(int damage)
    {
        if (canBeDamaged)
        {
            life -= damage;

            if(playerNum == PlayerNum.Player1)
                UIManager.instance.SetPlayer1HP(life);
            else
                UIManager.instance.SetPlayer2HP(life);

            if (life <= 0)
                KillPlayer();

            StartCoroutine("SetInvulnerable");
        }
    }

    private IEnumerator SetInvulnerable()
    {
        canBeDamaged = false;

        foreach (Renderer item in rendererList)
            item.material.color = Color.red;

        yield return new WaitForSeconds(0.3f);

        normalColor.a = 0.5f;
        foreach (Renderer item in rendererList)
            item.material.color = normalColor;

        yield return new WaitForSeconds(1.5f);

        normalColor.a = 1f;
        canBeDamaged = true;

        foreach (Renderer item in rendererList)
            item.material.color = normalColor;
    }

    //public void ApplyBombPowerUp(int extraBomb)
    //{
    //    bombStack += extraBomb;

    //    if (playerNum == PlayerNum.Player1)
    //        UIManager.instance.SetPlayer1Bombs(bombStack);
    //    else
    //        UIManager.instance.SetPlayer2Bombs(bombStack);
    //}

    public void ApplyRangePowerUp(int extraRange)
    {
        bombRange += extraRange;
        if (bombRange > 5)
            bombRange = 5;
    }

    public void ApplySpeedPowerUp(float extraSpeed)
    {
        speed *= extraSpeed;
        if (speed > 6)
            speed = 6;
    }

    public void ApplyLifePowerUp(int lifeSum)
    {
        life += lifeSum;

        if (playerNum == PlayerNum.Player1)
            UIManager.instance.SetPlayer1HP(life);
        else
            UIManager.instance.SetPlayer2HP(life);
    }

    private void KillPlayer()
    {
        animator.SetTrigger("Dead");

        PlayerInput input = GetComponent<PlayerInput>();
        input.enabled = false;

        LevelManager levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        levelManager.CheckLoseGame();

        StartCoroutine(DestroyAfterAnimation(1.15f));
    }

    private IEnumerator DestroyAfterAnimation(float seconds)
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = false;

        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }

    public enum PlayerNum
    {
        Player1,
        Player2,
    }
}