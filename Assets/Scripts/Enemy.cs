using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public EnemyType type;
    public float life = 3;
    public float speed = 3;
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

    public Animator animator;
    SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mapController = GameObject.Find("Grid Map").GetComponent<MapController>();
        sprite = GetComponent<SpriteRenderer>();
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

        bool canExit = false;

        do
        {
            int randomNum = Random.Range(0, 4);

            switch (randomNum)
            {
                case 0:
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
                        canExit = true;
                    }
                    break;

                case 1:
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
                        canExit = true;
                    }
                    break;

                case 2:
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
                        canExit = true;
                    }
                    break;

                case 3:
                    if (colLeft == null && direction != Vector2.left)
                    {
                        if (type == EnemyType.Walker)
                        {
                            animator.SetFloat("Up", 0);
                            animator.SetFloat("Right", 0);
                            animator.SetFloat("Left", 1);
                            sprite.flipX = false;
                        }
                        else if (type== EnemyType.Bomber)
                        {
                            animator.SetFloat("UpB", 0);
                            animator.SetFloat("RightB",0);
                            animator.SetFloat("LeftB",1);
                            sprite.flipX = false;
                        }
                        direction = Vector2.left;
                        canExit = true;
                    }
                    break;
            }

        } while (canExit == false);
    }

    private void ChangeTimerDirection()
    {
        // IMPORTANTE: Debe estar seteado el "Geometry Type" a "Polygons" en el Composite Collider del tile para que 
        // detecte correctamente las colisiones

        Vector3Int cell = mapController.GetCell(transform.position);
        Vector3 cellCenterPos = mapController.GetCellToWorld(cell);
        transform.position = cellCenterPos;

        bool canExit = false;

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
                        canExit = true;
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
                        canExit = true;
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
                        canExit = true;
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
                        canExit = true;
                    }
                    break;
            }

        } while (canExit == false);

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

    private IEnumerator SetInvulnerable()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1.5f);
        canTakeDamage = true;
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
