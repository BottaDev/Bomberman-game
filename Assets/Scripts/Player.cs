﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerNum playerNum;
    [Header("Player Settings")]
    public float life = 2;
    public float speed = 5;
    public int bombStack = 3;
    public float damage = 0.5f;
    public float attackRange = 0.5f;

    [Header("Bomb Settings")]
    [Range(min: 0.5f, max: 3f)]
    public float bombCd;
    [Range(min: 0.5f, max: 3f)]
    public float bombTimeToExplode = 3f;
    [Range(min: 2, max: 5)]
    public int bombRange = 2;   // Valor expresado en bloques del mapa

    private bool canBeDamaged = true;

    public void TakeDamage(float damage)
    {
        if (canBeDamaged)
        {
            life -= damage;

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

    public void ApplyPowerUp(int extraBomb)
    {
        bombStack = bombStack + extraBomb;
    }

    public void ApplyPowerUp2(int extraRange)
    {
        bombRange = bombRange + extraRange;
    }

    public void ApplyPowerUp3(float extraSpeed)
    {
        speed = speed * extraSpeed;
    }

    private void KillPlayer()
    {
        GameManager.instance.AddPlayerDeath(playerNum, this.gameObject);
        Destroy(gameObject);
    }

    public enum PlayerNum
    {
        Player1,
        Player2,
    }

}
