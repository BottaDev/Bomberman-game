using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public List<Renderer> rendererList = new List<Renderer>();      // Objetos que se pintaran de rojo al recibir daño
    [Header("Enemy Settings")]
    public EnemyType type;
    public float life = 3;
    public float speed = 2;
    public int damage = 1;
    public LayerMask collisionMask;

    protected Vector2 direction = Vector2.down; 
    protected bool canTakeDamage = true;
    protected Rigidbody2D rb;
    protected MapController mapController;
    
    protected Collider2D colUp;
    protected Collider2D colRight;
    protected Collider2D colDown;
    protected Collider2D colLeft;
    protected float directionTimer = 1.5f;
    protected SpriteRenderer sprite;
    protected Collider2D coll;

    [HideInInspector]
    public Animator animator;

    private Color normalColor;
    

    private void Start()
    {
        mapController = GameObject.Find("Grid Map").GetComponent<MapController>();

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        normalColor = rendererList[1].material.color;
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
            ChangeDirection(false);

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

    protected void ChangeDirection(bool onCollision = true)
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
                    if (onCollision)
                    {
                        canExitLoop = CheckDebugColision(debugTimes);

                        if (colUp == null && direction != Vector2.up)
                        {
                            SetAnimation(1, 0, 0, false);

                            direction = Vector2.up;
                            canExitLoop = true;
                        }
                        else
                            debugTimes++;
                    }
                    else
                    {
                        if (colUp == null && direction != Vector2.up && direction != Vector2.down)
                        {
                            SetAnimation(1, 0, 0, false);

                            direction = Vector2.up;
                            canExitLoop = true;
                        }
                    }                       
                    break;

                case 1:
                    if (onCollision)
                    {
                        canExitLoop = CheckDebugColision(debugTimes);

                        if (colRight == null && direction != Vector2.right)
                        {
                            SetAnimation(0, 1, 0, true);

                            direction = Vector2.right;
                            canExitLoop = true;
                            transform.localRotation = Quaternion.Euler(0, 180, 0);
                        }
                        else
                            debugTimes++;
                    }
                    else
                    {
                        if (colRight == null && direction != Vector2.right && direction != Vector2.left)
                        {
                            SetAnimation(0, 1, 0, true);

                            direction = Vector2.right;
                            canExitLoop = true;
                            transform.localRotation = Quaternion.Euler(0, 180, 0);
                        }
                    }
                    break;

                case 2:
                    if (onCollision)
                    {
                        canExitLoop = CheckDebugColision(debugTimes);

                        if (colDown == null && direction != Vector2.down)
                        {
                            SetAnimation(0, 0, 0, false);

                            direction = Vector2.down;
                            canExitLoop = true;
                        }
                        else
                            debugTimes++;
                    }
                    else
                    {
                        if (colDown == null && direction != Vector2.down && direction != Vector2.up)
                        {
                            SetAnimation(0, 0, 0, false);

                            direction = Vector2.down;
                            canExitLoop = true;
                        }
                    }                        
                    break;

                case 3:
                    if (onCollision)
                    {
                        canExitLoop = CheckDebugColision(debugTimes);

                        if (colLeft == null && direction != Vector2.left)
                        {
                            SetAnimation(0, 0, 1, false);

                            direction = Vector2.left;
                            canExitLoop = true;
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                        else
                            debugTimes++;
                    }
                    else
                    {
                        if (colLeft == null && direction != Vector2.left && direction != Vector2.right)
                        {
                            SetAnimation(0, 0, 1, false);

                            direction = Vector2.left;
                            canExitLoop = true;
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                    break;
            }
        } while (canExitLoop == false);

        if(!onCollision)
            directionTimer = 1.5f;
    }

    protected bool CheckDebugColision(int value)
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
    protected IEnumerator DebugInvulnerable()
    {
        coll.enabled = false;
        
        yield return new WaitForSeconds(0.5f);

        coll.enabled = true;
    }

    private IEnumerator SetInvulnerable()
    {
        canTakeDamage = false;

        foreach (Renderer item in rendererList)
            item.material.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        normalColor.a = 0.5f;
        foreach (Renderer item in rendererList)
            item.material.color = normalColor;

        yield return new WaitForSeconds(1.5f);
        
        canTakeDamage = true;
        normalColor.a = 1f;

        foreach (Renderer item in rendererList)
            item.material.color = normalColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();

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

    protected void SetAnimation(int up, int right, int left, bool flip)
    {
        if ((up > 1 || up < 0) || (right > 1 || right < 0) || (left > 1 || left < 0))
            Debug.LogError("Valores para animator incorrectos");

        if (type == EnemyType.Walker)
        {
            animator.SetFloat("Up", up);
            animator.SetFloat("Right", right);
            animator.SetFloat("Left", left);
            sprite.flipX = flip;
        }
        else if (type == EnemyType.Bomber)
        {
            animator.SetFloat("UpB", up);
            animator.SetFloat("RightB", right);
            animator.SetFloat("LeftB", left);
            sprite.flipX = flip;
        }
        else if (type == EnemyType.Jumper)
        {
            animator.SetFloat("UpC", up);
            animator.SetFloat("RightC", right);
            animator.SetFloat("LeftC", left);
            sprite.flipX = flip;
        }
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
