using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreSpeed : MonoBehaviour
{
    public float extraSpeed = 1.1f;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
            PickUp(collision);
    }

    void PickUp(Collider2D player)
    {
        source.Play();

        Player speed = player.GetComponent<Player>();

        speed.ApplySpeedPowerUp(extraSpeed);

        Destroy(gameObject);
    }
}
