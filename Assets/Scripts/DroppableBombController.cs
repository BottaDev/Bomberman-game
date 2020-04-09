using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DroppableBombController : MonoBehaviour
{
    public GameObject bombGO;

    private Player player;
    private float currentBombCd = 0;
    private Tilemap tilemap;
    public int bombStack = 3;

    private void Start()
    {
        player = GetComponent<Player>();
        tilemap = GameObject.Find("Blocks").GetComponent<Tilemap>();
    }

    private void Update()
    {
        currentBombCd -= Time.deltaTime;
        if (currentBombCd <= 0)
            currentBombCd = 0;

        if (Input.GetKeyDown(KeyCode.Space) && currentBombCd <= 0)
        {
            if (bombStack > 0)
            {
                DropBomb();
                bombStack = bombStack - 1;
            }

        }
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

        currentBombCd = player.bombCd;
    }
}
