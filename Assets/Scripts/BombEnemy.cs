using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    [Header("Bomb Settings")]
    public float explosionCd = 8f;
    public float timeToExplode = 1.5f;
    public int explosionRange = 3;

    private float currentExplosionCd = 0;
    private float currentTimeToExplode = 0;

    private void Awake()
    {
        currentExplosionCd = explosionCd;
        currentTimeToExplode = timeToExplode;
    }

    private void FixedUpdate()
    {
        currentExplosionCd -= Time.fixedDeltaTime;
        if (currentExplosionCd <= 0)
            currentExplosionCd = 0;

        if (currentExplosionCd > 0)
            MoveSimple();
        else
            PrepareToExplode();
    }

    private void PrepareToExplode()
    {
        animator.SetFloat("Attack",1);
        currentTimeToExplode -= Time.deltaTime;
        if (currentTimeToExplode <= 0)
            Explode();
        else
        {
            Vector3Int cell = mapController.GetCell(transform.position);
            Vector3 cellCenterPos = mapController.GetCellToWorld(cell);
            transform.position = cellCenterPos;
        }
    }

    private void Explode()
    {
        currentExplosionCd = explosionCd;
        currentTimeToExplode = timeToExplode;
        animator.SetFloat("Attack", 0);

        StartCoroutine("SetBombInvulnerable");

        GetComponent<MapDestroyer>().Explode(transform.position, explosionRange);
    }

    private IEnumerator SetBombInvulnerable()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(0.7f);
        
        canTakeDamage = true;
    }
}
