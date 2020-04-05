using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float life = 2;
    public float speed = 5;

    [Header("Bomb Settings")]
    [Range(min: 0.5f, max: 3f)]
    public float bombCd;
    [Range(min: 0.5f, max: 3f)]
    public float bombTimeToExplode = 3f;
    [Range(min: 2, max: 5)]
    public int bombRange = 2;   // Valor expresado en bloques del mapa

    private CircleCollider2D collider;

    private void Start()
    {
        collider = this.GetComponent<CircleCollider2D>();
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine("SetInvulnerable");
    }

    IEnumerator SetInvulnerable()
    {
        collider.enabled = false; 
        yield return new WaitForSeconds(2);
        collider.enabled = true;
    }
}
