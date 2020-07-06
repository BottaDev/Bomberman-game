using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DroppableBombController : MonoBehaviour
{
    public GameObject bombGO;
    public string inputBomb;

    private Player player;
    private float currentBombCd = 0;
    private Tilemap tilemap;
    private float auxInputBomb;

    private void Start()
    {
        player = GetComponent<Player>();
        tilemap = GameObject.Find("Blocks").GetComponent<Tilemap>();
    }

    private void Update()
    {
        auxInputBomb = Input.GetAxis(inputBomb);

        currentBombCd -= Time.deltaTime;
        if (currentBombCd <= 0)
            currentBombCd = 0;

        if (auxInputBomb == 1 && currentBombCd <= 0)
            DropBomb();
    }

    private void DropBomb()
    {
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        GameObject bomb = Instantiate(bombGO, cellCenterPos, Quaternion.identity);
        BombController bombController = bomb.GetComponent<BombController>();
        bombController.timeToExplode = player.bombTimeToExplode;
        bombController.explosionRange = player.bombRange;
        bombController.StartCoroutine("Explode", true);

        player.audioSource.clip = player.bombAction;
        player.audioSource.Play();

        currentBombCd = player.bombCd;
    }
}
