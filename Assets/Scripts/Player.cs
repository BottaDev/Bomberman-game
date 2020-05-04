using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerNum playerNum;
    [Header("Player Settings")]
    public int life = 2;
    public float speed = 5;
    public int bombStack = 3;

    [Header("Attack Settings")]
    public float damage = 0.5f;
    public float attackCd;
    public float attackRange = 0.2f;

    [Header("Bomb Settings")]
    [Range(min: 0.5f, max: 3f)]
    public float bombCd;
    [Range(min: 0.5f, max: 3f)]
    public float bombTimeToExplode = 3f;
    [Range(min: 2, max: 5)]
    public int bombRange = 2;   // Valor expresado en bloques del mapa

    private bool canBeDamaged = true;

    private void Start()
    {
        if (playerNum == PlayerNum.Player1)
        {
            UIManager.instance.SetPlayer1Bombs(bombStack);
            UIManager.instance.SetPlayer1HP(life);
        }
        else
        {
            UIManager.instance.SetPlayer2Bombs(bombStack);
            UIManager.instance.SetPlayer2HP(life);
        }
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

        yield return new WaitForSeconds(1.5f);

        canBeDamaged = true;
    }

    public void ApplyBombPowerUp(int extraBomb)
    {
        bombStack += extraBomb;

        if (playerNum == PlayerNum.Player1)
            UIManager.instance.SetPlayer1Bombs(bombStack);
        else
            UIManager.instance.SetPlayer2Bombs(bombStack);
    }

    public void ApplyRangePowerUp(int extraRange)
    {
        bombRange += extraRange;
    }

    public void ApplySpeedPowerUp(float extraSpeed)
    {
        speed *= extraSpeed;
    }

    private void KillPlayer()
    {
        GameManager.instance.AddPlayerDeath(playerNum, this.gameObject);

        LevelManager levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        levelManager.CheckLoseGame();

        Destroy(gameObject);
    }

    public enum PlayerNum
    {
        Player1,
        Player2,
    }

}
