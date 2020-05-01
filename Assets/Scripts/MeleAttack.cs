using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MeleAttack : MonoBehaviour
{
    public Tile destructibleTile;
    public string inputMele;
    public Transform attackPosition;
    public LayerMask allLayers;
    public GameObject direction;

    private Player player;
    private PlayerMovement playerMovement;
    private float auxInputMele;
    private MapController mapController;
    public float auxAttackCd = 0;

    private void Start()
    {
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
            direction.transform.localPosition = new Vector3(-0.1f, 0, 0);
    }

    void Attack()
    {
        Collider2D hitSomething = Physics2D.OverlapCircle(attackPosition.position, player.attackRange, allLayers);
        print("Entro");
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
