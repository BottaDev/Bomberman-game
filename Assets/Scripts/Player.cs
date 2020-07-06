using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PlayerNum playerNum;
    public List<Renderer> rendererList = new List<Renderer>();      // Objetos que se pintaran de rojo al recibir daño
    [Header("Audio")]
    public AudioClip hitSound;
    public AudioClip powerUpSound;
    public AudioClip bombAction;
    [Header("Player Settings")]
    public int life = 3;
    [Range(min:4, max: 6)]
    public float speed = 4;

    [Header("Bomb Settings")]
    [Range(min: 0.5f, max: 3f)]
    public float bombCd = 3;
    [Range(min: 0.5f, max: 3f)]
    public float bombTimeToExplode = 3f;
    [Range(min: 1, max: 5)]
    public int bombRange = 2;   // Valor expresado en bloques del mapa
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public AudioSource audioSource;

    private TutorialManager tutorialManager;
    private bool canBeDamaged = true;
    private Color normalColor;

    private void Start()
    {
        // Si la escena no es tutorial...
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            if (playerNum == PlayerNum.Player1)
                UIManager.instance.SetPlayer1HP(life);
            else
                UIManager.instance.SetPlayer2HP(life);
        }
        else
        {
            tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
            life = 20;
            tutorialManager.SetUiHp(life);
        }

        animator = GetComponent<Animator>();

        normalColor = rendererList[1].material.color;

        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (canBeDamaged)
        {
            life -= damage;

            // Si la escena no es tutorial...
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                if(playerNum == PlayerNum.Player1)
                    UIManager.instance.SetPlayer1HP(life);
                else
                    UIManager.instance.SetPlayer2HP(life);
            }
            else
            {
                tutorialManager.SetUiHp(life);
            }

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

        audioSource.clip = hitSound;
        audioSource.Play();

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

    public void ApplyRangePowerUp(int extraRange)
    {
        audioSource.clip = powerUpSound;
        audioSource.Play();

        bombRange += extraRange;
        if (bombRange > 5)
            bombRange = 5;
    }

    public void ApplySpeedPowerUp(float extraSpeed)
    {
        audioSource.clip = powerUpSound;
        audioSource.Play();

        speed *= extraSpeed;
        if (speed > 6)
            speed = 6;
    }

    public void ApplyLifePowerUp(int lifeSum)
    {
        audioSource.clip = powerUpSound;
        audioSource.Play();

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