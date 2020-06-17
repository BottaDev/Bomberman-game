﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreLife : MonoBehaviour
{
    public int life = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
            PickUp(collision);
    }

    void PickUp(Collider2D player)
    {
        Player range = player.GetComponent<Player>();

        range.ApplyLifePowerUp(life);

        Destroy(gameObject);
    }
}
