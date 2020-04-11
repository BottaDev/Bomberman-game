using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public EnemyType type;
    public float life = 3;
    public float speed = 3;
    public float damage = 1;
    public LayerMask collisionMask;

    private Vector2 direction = Vector2.down; 
    private bool canTakeDamage = true;
    private Rigidbody2D rb;
    private Tilemap tilemap;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tilemap = GameObject.Find("Blocks").GetComponent<Tilemap>();
    }

    private void Update()
    {
        Move();
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            life -= damage;

            if (life <= 0)
                Destroy(gameObject);

            StartCoroutine("SetInvulnerable");
        }
    }

    public virtual void Move()
    {
        Vector3 dir = new Vector3();
        dir = direction;
        rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        // IMPORTANTE: Debe estar seteado el "Geometry Type" a "Polygons" en el Composite Collider del tile para que 
        // detecte correctamente las colisiones

        Collider2D colUp = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, 0.7f), 0.15f);
        Collider2D colRight = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0.7f, 0), 0.15f );
        Collider2D colDown = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.7f), 0.15f);
        Collider2D colLeft = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(-0.7f, 0), 0.15f);

        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
        transform.position = cellCenterPos;

        bool canExit = false;

        do
        {
            int randomNum = Random.Range(0, 4);

            switch (randomNum)
            {
                case 0:
                    if (colUp == null)
                    {
                        direction = Vector2.up;
                        canExit = true;
                    }
                    break;

                case 1:
                    if (colRight == null)
                    {
                        direction = Vector2.right;
                        canExit = true;
                    }
                    break;

                case 2:
                    if (colDown == null)
                    {
                        direction = Vector2.down;
                        canExit = true;
                    }
                    break;

                case 3:
                    if (colLeft == null)
                    {
                        direction = Vector2.left;
                        canExit = true;
                    }
                    break;
            }

        } while (canExit == false);
    }

    private IEnumerator SetInvulnerable()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1.5f);
        canTakeDamage = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bloques
        //if (collision.gameObject.layer == 8)
        //    ChangeDirection();
        ChangeDirection();
        // Player
        if (collision.gameObject.layer == 9)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }
            
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2) transform.position + new Vector2(0, 0.7f), 0.15f);
        Gizmos.DrawWireSphere((Vector2) transform.position + new Vector2(0.7f, 0), 0.15f);
        Gizmos.DrawWireSphere((Vector2) transform.position + new Vector2(0, -0.7f), 0.15f);
        Gizmos.DrawWireSphere((Vector2) transform.position + new Vector2(-0.7f, 0), 0.15f);
    }

    public enum EnemyType { Walker, Jumper, Bomber, Glutton, Ghost };
}
