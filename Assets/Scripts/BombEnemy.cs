using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    [Header("Bomb Settings")]
    public float explosionCd = 8f;
    public float timeToExplode = 1.5f;
    public int explosionRange = 3;

    [SerializeField]
    private float currentExplosionCd = 0;
    [SerializeField]
    private float currentTimeToExplode = 0;

    private void Awake()
    {
        currentExplosionCd = explosionCd;
        currentTimeToExplode = timeToExplode;
    }

    private void FixedUpdate()
    {
        currentExplosionCd -= Time.deltaTime;
        if (currentExplosionCd <= 0)
            currentExplosionCd = 0;

        if (currentExplosionCd > 0)
            MoveSimple();
        else
            PrepareToExplode();
    }

    private void PrepareToExplode()
    {
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

        StartCoroutine("SetInvulnerable");

        GetComponent<MapDestroyer>().Explode(transform.position, explosionRange);
    }
}
