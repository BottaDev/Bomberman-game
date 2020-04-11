using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float duration = 0.5f;
    public float damage = 1;

    void Start()
    {
        Destroy(gameObject,duration);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player collision
        if (collision.gameObject.layer == 9)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }

        // Enemy collision
        if (collision.gameObject.layer == 11)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        } 
    }
}
