using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public string inputAxisX;
    public string inputAxisY;

    public Animator animator;

    Player player;
    Rigidbody2D rb;
    PlayerMovement playerMovement;

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
    public GameObject direction;

    private float auxInputMele;
    private MapController mapController;
    private float auxAttackCd = 0;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
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

        if (playerMovement.moveUp == true)
            direction.transform.localPosition = new Vector3(0, 0.1f, 0);
        if (playerMovement.moveDown == true)
            direction.transform.localPosition = new Vector3(0, -0.1f, 0);
        if (playerMovement.moveRight == true)
            direction.transform.localPosition = new Vector3(0.1f, 0, 0);
        if (playerMovement.moveLeft == true)
            direction.transform.localPosition = new Vector3(0.1f, 0, 0);
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
            animator.SetFloat("MoveU", 1);
            animator.SetFloat("MoveD", 0);
            animator.SetFloat("MoveL", 0);
            animator.SetFloat("MoveR", 0);
            moveUp = true;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
        }
        else if (auxAxisY < 0)
        {
            rb.MovePosition(transform.position + new Vector3(0, -1, 0) * player.speed * Time.deltaTime);
            animator.SetFloat("MoveD", 1);
            animator.SetFloat("MoveU", 0);
            animator.SetFloat("MoveL", 0);
            animator.SetFloat("MoveR", 0);
            moveUp = false;
            moveDown = true;
            moveLeft = false;
            moveRight = false;
        }
        else if(auxAxisX < 0)
        {
            rb.MovePosition(transform.position + new Vector3(-1, 0, 0) * player.speed * Time.deltaTime);
            animator.SetFloat("MoveL", 1);
            animator.SetFloat("MoveU", 0);
            animator.SetFloat("MoveD", 0);
            animator.SetFloat("MoveR", 0);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            moveUp = false;
            moveDown = false;
            moveLeft = true;
            moveRight = false;
        }
        else if (auxAxisX > 0)
        {
            rb.MovePosition(transform.position + new Vector3(1, 0, 0) * player.speed * Time.deltaTime);
            animator.SetFloat("MoveR", 1);
            animator.SetFloat("MoveU", 0);
            animator.SetFloat("MoveD", 0);
            animator.SetFloat("MoveL", 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = true;
        }
        else
        {
            animator.SetFloat("MoveR", 0);
            animator.SetFloat("MoveU", 0);
            animator.SetFloat("MoveD", 0);
            animator.SetFloat("MoveL", 0);
        }
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
                auxAttackCd = player.attackCd;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPosition == null)
            return;

        Gizmos.DrawWireSphere(attackPosition.position, player.attackRange);
    }
}