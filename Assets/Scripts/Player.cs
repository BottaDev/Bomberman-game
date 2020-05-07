﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerNum playerNum;
    [Header("Player Settings")]
    public int life = 3;
    public float speed = 3;
    public int bombStack = 3;

    [Header("Attack Settings")]
    public float damage = 0.5f;
    public float attackCd;
    public float attackRange = 0.2f;

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
    private Renderer rend;
    private Color normalColor;

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

        animator = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        normalColor = rend.material.color;
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

        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.3f);

        normalColor.a = 0.5f;
        rend.material.color = normalColor;

        yield return new WaitForSeconds(1.5f);

        normalColor.a = 1f;
        canBeDamaged = true;
        rend.material.color = normalColor;
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