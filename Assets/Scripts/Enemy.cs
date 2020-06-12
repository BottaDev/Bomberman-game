using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public EnemyType type;
    public float life = 3;
    public float speed = 2;
    public int damage = 1;
    public LayerMask collisionMask;

    protected Vector2 direction = Vector2.down; 
    protected bool canTakeDamage = true;
    [SerializeField]
    protected Rigidbody2D rb;
    protected MapController mapController;

    protected Collider2D colUp;
    protected Collider2D colRight;
    protected Collider2D colDown;
    protected Collider2D colLeft;
    protected float directionTimer = 1.5f;

    [HideInInspector]
    public Animator animator;

    private Renderer rend;
    private Color normalColor;
    private Collider2D coll;

    private void Start()
    {
        mapController = GameObject.Find("Grid Map").GetComponent<MapController>();

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        normalColor = rend.material.color;
    }

    private void FixedUpdate()
    {
        MoveSimple();
    }

    protected void MoveSimple()
    {
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0)
            directionTimer = 0;

        bool canChangeDirection = DetectCollisions();

        int randomNum = Random.Range(0, 4);
        if (canChangeDirection && directionTimer == 0f && randomNum == 0)
            ChangeTimerDirection();

        // Si todos los colliders detectan algo, no se deberia mover
        if (colUp != null && colRight != null && colDown != null && colLeft != null)
            return;
        else
            Move();
    }

    private void Move()
    {
        Vector3 dir;
        dir = direction;
        rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
    }

    private bool DetectCollisions()
    {
        colUp = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, 0.7f), 0.15f, collisionMask);
        colRight = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0.7f, 0), 0.15f, collisionMask);
        colDown = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.7f), 0.15f, collisionMask);
        colLeft = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(-0.7f, 0), 0.15f, collisionMask);

        int colCount = 0;
        bool canChangeDirection = false;

        if (colUp == null)
            colCount++;
        if (colRight == null)
            colCount++;
        if (colDown == null)
            colCount++;
        if (colLeft == null)
            colCount++;

        if (colCount >= 3)
            canChangeDirection = true;

        return canChangeDirection;
    }

    private void ChangeColisionDirection()
    {
        // IMPORTANTE: Debe estar seteado el "Geometry Type" a "Polygons" en el Composite Collider del tile para que 
        // detecte correctamente las colisiones

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
                        }
                        else if (type == EnemyType.Bomber)
                        {
                            animator.SetFloat("UpB", 1);
                            animator.SetFloat("RightB", 0);
                            animator.SetFloat("LeftB", 0);
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
                            transform.localRotation = Quaternion.Euler(0, 180, 0);
                        }
                        else if (type == EnemyType.Bomber)
                        {
                            animator.SetFloat("UpB", 0);
                            animator.SetFloat("RightB", 1);
                            animator.SetFloat("LeftB", 0);
                            transform.localRotation = Quaternion.Euler(0, 180, 0);
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
                        }
                        else if (type == EnemyType.Bomber)
                        {
                            animator.SetFloat("UpB", 0);
                            animator.SetFloat("RightB", 0);
                            animator.SetFloat("LeftB", 0);
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
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                        else if (type == EnemyType.Bomber)
                        {
                            animator.SetFloat("UpB", 0);
                            animator.SetFloat("RightB",0);
                            animator.SetFloat("LeftB",1);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
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

    private bool CheckDebugColision(int value)
    {
        if (value >= 40)
        {
            StartCoroutine("DebugInvulnerable");
            Debug.LogWarning("Colision infinita corregida");

            return true;
        }
        else
            return false;
    }

    private void ChangeTimerDirection()
    {
        // IMPORTANTE: Debe estar seteado el "Geometry Type" a "Polygons" en el Composite Collider del tile para que 
        // detecte correctamente las colisiones

        Vector3Int cell = mapController.GetCell(transform.position);
        Vector3 cellCenterPos = mapController.GetCellToWorld(cell);
        transform.position = cellCenterPos;

        bool canExitLoop = false;

        do
        {
            int randomNum = Random.Range(0, 4);

            switch (randomNum)
            {
                case 0:
                    if (colUp == null && direction != Vector2.up && direction != Vector2.down)
                    {
                        if (type == EnemyType.Walker)
                        {
                            animator.SetFloat("Up", 1);
                            animator.SetFloat("Right", 0);
                            animator.SetFloat("Left", 0);
                        }
                        else if (type == EnemyType.Bomber)
                        {
                            animator.SetFloat("UpB", 1);
                            animator.SetFloat("RightB", 0);
                            animator.SetFloat("LeftB", 0);
                        }
                        direction = Vector2.up;
                        canExitLoop = true;
                    }
                    break;

                case 1:
                    if (colRight == null && direction != Vector2.right && direction != Vector2.left)
                    {
                        if (type == EnemyType.Walker)
                        {
                            animator.SetFloat("Up", 0);
                            animator.SetFloat("Right", 1);
                            animator.SetFloat("Left", 0);
                            transform.localRotation = Quaternion.Euler(0, 180, 0);
                        }
                        else if (type == EnemyType.Bomber)
                        {
                            animator.SetFloat("UpB", 0);
                            animator.SetFloat("RightB", 1);
                            animator.SetFloat("LeftB", 0);
                            transform.localRotation = Quaternion.Euler(0, 180, 0);
                        }
                        direction = Vector2.right;
                        canExitLoop = true;
                    }
                    break;

                case 2:
                    if (colDown == null && direction != Vector2.down && direction != Vector2.up)
                    {
                        if (type == EnemyType.Walker)
                        {
                            animator.SetFloat("Up", 0);
                            animator.SetFloat("Right", 0);
                            animator.SetFloat("Left", 0);
                        }
                        else if (type == EnemyType.Bomber)
                        {
                            animator.SetFloat("UpB", 0);
                            animator.SetFloat("RightB", 0);
                            animator.SetFloat("LeftB", 0);
                        }
                        direction = Vector2.down;
                        canExitLoop = true;
                    }
                    break;

                case 3:
                    if (colLeft == null && direction != Vector2.left && direction != Vector2.right)
                    {
                        if (type == EnemyType.Walker)
                        {
                            animator.SetFloat("Up", 0);
                            animator.SetFloat("Right", 0);
                            animator.SetFloat("Left", 1);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                        else if (type == EnemyType.Bomber)
                        {
                            animator.SetFloat("UpB", 0);
                            animator.SetFloat("RightB", 0);
                            animator.SetFloat("LeftB", 1);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                        direction = Vector2.left;
                        canExitLoop = true;
                    }
                    break;
            }

        } while (canExitLoop == false);

        directionTimer = 1.5f;
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            life -= damage;

            if (life <= 0)
                KillEnemy();

            StartCoroutine("SetInvulnerable");
        }
    }

    // Se llama a esta funcion para evitar el loop infinito al colocar una bomba muy cerca de un enemigo
    private IEnumerator DebugInvulnerable()
    {
        coll.enabled = false;
        
        yield return new WaitForSeconds(0.5f);

        coll.enabled = true;
    }

    private IEnumerator SetInvulnerable()
    {
        canTakeDamage = false;

        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);

        normalColor.a = 0.5f;
        rend.material.color = normalColor;

        yield return new WaitForSeconds(1.5f);
        
        canTakeDamage = true;
        normalColor.a = 1f;
        rend.material.color = normalColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeColisionDirection();

        // Player
        if (collision.gameObject.layer == 9)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }
            
    }

    private void KillEnemy()
    {
        LevelManager levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        levelManager.AddEnemyDeath();

        Destroy(gameObject);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2) transform.position + new Vector2(0, 0.7f), 0.15f);
        Gizmos.DrawWireSphere((Vector2) transform.position + new Vector2(0.7f, 0), 0.15f);
        Gizmos.DrawWireSphere((Vector2) transform.position + new Vector2(0, -0.7f), 0.15f);
        Gizmos.DrawWireSphere((Vector2) transform.position + new Vector2(-0.7f, 0), 0.15f);
    }

    public enum EnemyType { Walker, Jumper, Bomber, Glutton, Ghost };
}
