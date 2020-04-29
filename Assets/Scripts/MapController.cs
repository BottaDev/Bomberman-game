﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
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

    // Retorna la celda donde se origino la explosion de la bomba
    public Vector3Int GetOriginCell(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        return originCell;
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
}
