using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public GameObject moreRangeGO;
    public GameObject moreSpeedGO;
    public GameObject moreLifeGO;

    private Tilemap blocksTilemap;
    private Tilemap groundTilemap;

    private void Start()
    {
        blocksTilemap = GameObject.Find("Blocks").GetComponent<Tilemap>();
        groundTilemap = GameObject.Find("Ground TM").GetComponent<Tilemap>();

        if (blocksTilemap == null)
        {
            Debug.Log("Error. No se encontró el objeto 'Blocks'");
            return;
        }
    }

    public void DestroyCell(Vector3Int cell)
    {
        blocksTilemap.SetTile(cell, null);
    }

    public Vector3Int GetCell(Vector2 worldPos)
    {
        Vector3Int cell = blocksTilemap.WorldToCell(worldPos);
        return cell;
    }
    
    public Vector3 GetCellToWorld(Vector3Int cell)
    {
        Vector3 position = blocksTilemap.GetCellCenterWorld(cell);
        return position;
    }

    public Tile GetTile(Vector3Int cell, bool needGround = false)
    {
        if (needGround)
        {
            Tile tile = groundTilemap.GetTile<Tile>(cell);
            return tile;
        }
        else
        {
            Tile tile = blocksTilemap.GetTile<Tile>(cell);
            return tile;
        }
    }

    public void DropPowerUp(Vector3Int cell)
    {
        int randomProbability = Random.Range(1, 21);
        Vector3 cellPosition = GetCellToWorld(cell);

        switch (randomProbability)
        {
            case 40:
            case 41:
            case 42:
            case 43:
            case 44:
            case 45:
            case 46:
            case 47:
            case 48:
            case 49:
            case 50:
                PowerUpType(cellPosition);
                break;
        }
    }

    private void PowerUpType(Vector3 cellPosition)
    {
        int randomProbabilityPowerUp = Random.Range(1, 4);

        switch (randomProbabilityPowerUp)
        {
            case 1:
                Instantiate(moreRangeGO, cellPosition, Quaternion.identity);
                break;

            case 2:
                Instantiate(moreSpeedGO, cellPosition, Quaternion.identity);
                break;

            case 3:
                Instantiate(moreLifeGO, cellPosition, Quaternion.identity);
                break;
        }
    }
}
