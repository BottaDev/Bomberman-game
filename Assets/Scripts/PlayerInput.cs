using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    [Header("Movimiento")]
    public string inputAxisX;
    public string inputAxisY;

    Player player;
    Rigidbody2D rb;

    [HideInInspector]
    public bool moveUp;
    [HideInInspector]
    public bool moveDown;
    [HideInInspector]
    public bool moveLeft;
    [HideInInspector]
    public bool moveRight;

    private float auxAxisX;
    private float auxAxisY;

    [Header("Ataque Mele")]
    public Tile destructibleTile;
    public string inputMele;
    public Transform attackPosition;
    public LayerMask allLayers;
    public Transform direction;

    private float auxInputMele;
    private MapController mapController;
    private float auxAttackCd = 0;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        mapController = GameObject.Find("Grid Map").GetComponent<MapController>();

        if (mapController == null)
        {
            Debug.Log("Error. No se encontró el objeto 'Grid Map'");
            return;
        }
    }

    private void Update()
    {
        auxInputMele = Input.GetAxis(inputMele);

        auxAttackCd -= Time.deltaTime;
        if (auxAttackCd < 0)
            auxAttackCd = 0;

        if (auxInputMele == 1 && auxAttackCd <= 0)
            Attack();
        
        if (moveUp == true)
            direction.localPosition = new Vector3(0, 0.1f, 0);
        if (moveDown == true)
            direction.localPosition = new Vector3(0, -0.1f, 0);
        if (moveRight == true)
            direction.localPosition = new Vector3(0.1f, 0, 0);
        if (moveLeft == true)
            direction.localPosition = new Vector3(0.1f, 0, 0);
    }

    private void FixedUpdate()
    {
        auxAxisX = Input.GetAxis(inputAxisX);
        auxAxisY = Input.GetAxis(inputAxisY);

        ProcessInput();
    }

    void ProcessInput()
    {
        auxInputMele = Input.GetAxis(inputMele);

        auxAttackCd -= Time.deltaTime;
        if (auxAttackCd < 0)
            auxAttackCd = 0;

        if (auxInputMele == 1 && auxAttackCd <= 0)
            Attack();

        else if (auxAxisY > 0)
        {
            rb.MovePosition(transform.position + new Vector3(0, 1, 0) * player.speed * Time.deltaTime);
            player.animator.SetFloat("MoveU", 1);
            player.animator.SetFloat("MoveD", 0);
            player.animator.SetFloat("MoveL", 0);
            player.animator.SetFloat("MoveR", 0);
            moveUp = true;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
        }
        else if (auxAxisY < 0)
        {
            rb.MovePosition(transform.position + new Vector3(0, -1, 0) * player.speed * Time.deltaTime);
            player.animator.SetFloat("MoveD", 1);
            player.animator.SetFloat("MoveU", 0);
            player.animator.SetFloat("MoveL", 0);
            player.animator.SetFloat("MoveR", 0);
            moveUp = false;
            moveDown = true;
            moveLeft = false;
            moveRight = false;
        }
        else if(auxAxisX < 0)
        {
            rb.MovePosition(transform.position + new Vector3(-1, 0, 0) * player.speed * Time.deltaTime);
            player.animator.SetFloat("MoveL", 1);
            player.animator.SetFloat("MoveU", 0);
            player.animator.SetFloat("MoveD", 0);
            player.animator.SetFloat("MoveR", 0);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            moveUp = false;
            moveDown = false;
            moveLeft = true;
            moveRight = false;
        }
        else if (auxAxisX > 0)
        {
            rb.MovePosition(transform.position + new Vector3(1, 0, 0) * player.speed * Time.deltaTime);
            player.animator.SetFloat("MoveR", 1);
            player.animator.SetFloat("MoveU", 0);
            player.animator.SetFloat("MoveD", 0);
            player.animator.SetFloat("MoveL", 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = true;
        }
        else
        {
            player.animator.SetFloat("MoveR", 0);
            player.animator.SetFloat("MoveU", 0);
            player.animator.SetFloat("MoveD", 0);
            player.animator.SetFloat("MoveL", 0);
        }
    }

    private void ExcuteAttackAnimation()
    {
        if (moveUp == true)
            player.animator.SetTrigger("PunchU");
        else if (moveDown == true)
            player.animator.SetTrigger("PunchD");
        else if (moveRight == true)
            player.animator.SetTrigger("PunchR");
        else if (moveLeft == true)
            player.animator.SetTrigger("PunchL");
    }

    void Attack()
    {

        Collider2D hitSomething = Physics2D.OverlapCircle(attackPosition.position, player.attackRange, allLayers);

        // Enemy
        if (hitSomething != null && hitSomething.gameObject.layer == 11)
        {
            Enemy enemy = hitSomething.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(player.damage);
        }
        // Wall
        else if (hitSomething != null && hitSomething.gameObject.layer == 8)
        {
            Vector3Int cell = mapController.GetCell(attackPosition.position);
            Tile tile = mapController.GetTile(cell);

            if (tile == destructibleTile)
            {
                mapController.DestroyCell(cell);
                mapController.DropPowerUp(cell);
            }
        }
        ExcuteAttackAnimation();
        auxAttackCd = player.attackCd;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPosition == null)
            return;

        Gizmos.DrawWireSphere(attackPosition.position, player.attackRange);
    }
}