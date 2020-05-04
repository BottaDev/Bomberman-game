using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public GameObject bombPlusGO;
    public GameObject moreRangeGO;
    public GameObject moreSpeedGO;

    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GameObject.Find("Blocks").GetComponent<Tilemap>();

        if (tilemap == null)
        {
            Debug.Log("Error. No se encontró el objeto 'Blocks'");
            return;
        }
    }

    public void DestroyCell(Vector3Int cell)
    {
        tilemap.SetTile(cell, null);
    }

    public Vector3Int GetCell(Vector2 worldPos)
    {
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        return cell;
    }
    
    public Vector3 GetCellToWorld(Vector3Int cell)
    {
        Vector3 position = tilemap.GetCellCenterWorld(cell);
        return position;
    }

    public Tile GetTile(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);
        return tile;
    }

    public void DropPowerUp(Vector3Int cell)
    {
        int randomProbability = Random.Range(1, 11);
        Vector3 cellPosition = GetCellToWorld(cell);

        switch (randomProbability)
        {
            case 6:
            case 7:
            case 8:
                Instantiate(bombPlusGO, cellPosition, Quaternion.identity);
                break;

            case 9:
            case 10:
                PowerUpType(cellPosition);
                break;
        }
    }

    private void PowerUpType(Vector3 cellPosition)
    {
        int randomProbabilityPowerUp = Random.Range(1, 3);

        switch (randomProbabilityPowerUp)
        {
            case 1:
                Instantiate(moreRangeGO, cellPosition, Quaternion.identity);
                break;

            case 2:
                Instantiate(moreSpeedGO, cellPosition, Quaternion.identity);
                break;
        }
    }
}
