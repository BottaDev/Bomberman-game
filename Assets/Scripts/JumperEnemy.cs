﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JumperEnemy : Enemy
{
    [Header("Jump Settings")]
    public float jumpSpeed = 2;
    public float jumpCd = 5;
    public Tile groundTile;

    [SerializeField]
    private float currentJumpCd;

    private void Awake()
    {
        currentJumpCd = jumpCd;
    }

    private void Update()
    {
        currentJumpCd -= Time.deltaTime;
        if (currentJumpCd <= 0)
            currentJumpCd = 0;
    }

    private void CheckJump()
    {
        if (currentJumpCd <= 0)
        {
            Vector3Int cell = mapController.GetCell(transform.position);
            Vector3 cellToJump = GetCellToJump(cell);

            if (cellToJump.x != 9999)
                Jump(cellToJump);
            else
                ChangeDirection();
        }
        else
            ChangeDirection();
    }

    private Vector3 GetCellToJump(Vector3Int currentCell)
    {
        Tile cellGroundTile = null;
        Tile cellBlockTile = null;
        Vector3Int nextCell = Vector3Int.zero;

        if (direction == Vector2.up)
        {
            nextCell = currentCell + new Vector3Int(0, 2, 0);
            cellGroundTile = mapController.GetTile(nextCell, true);
            cellBlockTile = mapController.GetTile(nextCell, false);
        }
        else if (direction == Vector2.down)
        {
            nextCell = currentCell + new Vector3Int(0, -2, 0);
            cellGroundTile = mapController.GetTile(nextCell, true);
            cellBlockTile = mapController.GetTile(nextCell, false);
        }
        else if (direction == Vector2.right)
        {
            nextCell = currentCell + new Vector3Int(2, 0, 0);
            cellGroundTile = mapController.GetTile(nextCell, true);
            cellBlockTile = mapController.GetTile(nextCell, false);
        }   
        else if (direction == Vector2.left)
        {
            nextCell = currentCell + new Vector3Int(-2, 0, 0);
            cellGroundTile = mapController.GetTile(nextCell, true);
            cellBlockTile = mapController.GetTile(nextCell, false);
        }

        if (cellGroundTile != null && cellBlockTile == null && cellGroundTile == groundTile)
        {
            Vector3 cellCenterPos = mapController.GetCellToWorld(nextCell);
            return cellCenterPos;
        }

        // No se puede realizar el salto
        return new Vector3(9999, 0, 0);    
    }

    private void Jump(Vector3 cellToJump)
    {
        print("Saltó");
        coll.enabled = false;

        transform.position = Vector3.MoveTowards(transform.position, cellToJump, jumpSpeed * Time.deltaTime);
        
        // Acomoda el enemigo en la celda
        transform.position = cellToJump;

        coll.enabled = true;

        currentJumpCd = jumpCd;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == EnemyType.Jumper)
            CheckJump();
        else
            ChangeDirection();

        // Player
        if (collision.gameObject.layer == 9)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }
}
