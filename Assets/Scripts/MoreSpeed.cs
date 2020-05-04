using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreSpeed : MonoBehaviour
{
    public float extraSpeed = 1.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
            PickUp(collision);
    }

    void PickUp(Collider2D player)
    {
        Player speed = player.GetComponent<Player>();

        speed.ApplySpeedPowerUp(extraSpeed);

        Destroy(gameObject);
    }
}
