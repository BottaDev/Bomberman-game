using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JumperEnemy : Enemy
{
    [Header("Jump Settings")]
    public float jumpSpeed = 1;
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

    public override void ChangeColisionDirection()
    {
        // IMPORTANTE: Debe estar seteado el "Geometry Type" a "Polygons" en el Composite Collider del tile para que 
        // detecte correctamente las colisiones

        if (currentJumpCd <= 0)
        {
            Vector3Int cell = mapController.GetCell(transform.position);
            Vector3 cellToJump = GetCellToJump(cell);

            if (cellToJump.x != 9999)
            {
                currentJumpCd = jumpCd;
                Jump(cellToJump);
            }
            else
            {
                currentJumpCd = jumpCd;
                ChangeColisionDirection();
            }
        }
        else
        {
            Vector3Int cell = mapController.GetCell(transform.position);
            Vector3 cellCenterPos = mapController.GetCellToWorld(cell);
            transform.position = cellCenterPos;

            bool canExitLoop = false;
            int debugTimes = 0;             // Cantidad de veces que se ejecutó el loop

            do
            {
                int randomNum = Random.Range(0, 4);

                switch (randomNum)
                {
                    case 0:
                        canExitLoop = CheckDebugColision(debugTimes);

                        if (colUp == null && direction != Vector2.up)
                        {
                            if (type == EnemyType.Walker)
                            {
                                animator.SetFloat("Up", 1);
                                animator.SetFloat("Right", 0);
                                animator.SetFloat("Left", 0);
                                sprite.flipX = false;
                            }
                            else if (type == EnemyType.Bomber)
                            {
                                animator.SetFloat("UpB", 1);
                                animator.SetFloat("RightB", 0);
                                animator.SetFloat("LeftB", 0);
                                sprite.flipX = false;
                            }
                            direction = Vector2.up;
                            canExitLoop = true;
                        }
                        else
                            debugTimes++;
                        break;

                    case 1:
                        canExitLoop = CheckDebugColision(debugTimes);

                        if (colRight == null && direction != Vector2.right)
                        {
                            if (type == EnemyType.Walker)
                            {
                                animator.SetFloat("Up", 0);
                                animator.SetFloat("Right", 1);
                                animator.SetFloat("Left", 0);
                                sprite.flipX = true;
                            }
                            else if (type == EnemyType.Bomber)
                            {
                                animator.SetFloat("UpB", 0);
                                animator.SetFloat("RightB", 1);
                                animator.SetFloat("LeftB", 0);
                                sprite.flipX = true;
                            }
                            direction = Vector2.right;
                            canExitLoop = true;
                        }
                        else
                            debugTimes++;
                        break;

                    case 2:
                        canExitLoop = CheckDebugColision(debugTimes);

                        if (colDown == null && direction != Vector2.down)
                        {
                            if (type == EnemyType.Walker)
                            {
                                animator.SetFloat("Up", 0);
                                animator.SetFloat("Right", 0);
                                animator.SetFloat("Left", 0);
                                sprite.flipX = false;
                            }
                            else if (type == EnemyType.Bomber)
                            {
                                animator.SetFloat("UpB", 0);
                                animator.SetFloat("RightB", 0);
                                animator.SetFloat("LeftB", 0);
                                sprite.flipX = false;
                            }
                            direction = Vector2.down;
                            canExitLoop = true;
                        }
                        else
                            debugTimes++;
                        break;

                    case 3:
                        canExitLoop = CheckDebugColision(debugTimes);

                        if (colLeft == null && direction != Vector2.left)
                        {
                            if (type == EnemyType.Walker)
                            {
                                animator.SetFloat("Up", 0);
                                animator.SetFloat("Right", 0);
                                animator.SetFloat("Left", 1);
                                sprite.flipX = false;
                            }
                            else if (type == EnemyType.Bomber)
                            {
                                animator.SetFloat("UpB", 0);
                                animator.SetFloat("RightB", 0);
                                animator.SetFloat("LeftB", 1);
                                sprite.flipX = false;
                            }
                            direction = Vector2.left;
                            canExitLoop = true;
                        }
                        else
                            debugTimes++;
                        break;
                }
            } while (canExitLoop == false);
        }
    }

    private Vector3 GetCellToJump(Vector3Int currentCell)
    {
        Tile cellTile = null;
        Vector3Int nextCell = Vector3Int.zero;

        if (direction == Vector2.up)
        {
            nextCell = currentCell + new Vector3Int(0, 2, 0);
            cellTile = mapController.GetTile(nextCell, true);
        }
        else if (direction == Vector2.down)
        {
            nextCell = currentCell + new Vector3Int(0, -2, 0);
            cellTile = mapController.GetTile(nextCell, true);
        }
        else if (direction == Vector2.right)
        {
            nextCell = currentCell + new Vector3Int(2, 0, 0);
            cellTile = mapController.GetTile(nextCell, true);
        }   
        else if (direction == Vector2.left)
        {
            nextCell = currentCell + new Vector3Int(-2, 0, 0);
            cellTile = mapController.GetTile(nextCell, true);
        }

        if (cellTile != null && cellTile == groundTile)
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
        StartCoroutine("DebugInvulnerable");

        transform.position = Vector3.MoveTowards(transform.position, cellToJump, jumpSpeed * Time.deltaTime);

        // Acomoda el enemigo en la celda
        transform.position = cellToJump;
    }
}
