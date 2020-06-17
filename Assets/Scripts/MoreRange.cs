using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreRange : MonoBehaviour
{
    public int extraRange = 1;

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

        Player range = player.GetComponent<Player>();

        range.ApplyRangePowerUp(extraRange);

        Destroy(gameObject);
    }
}
