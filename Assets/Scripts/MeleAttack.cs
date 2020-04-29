using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    public string inputMele;
    public Transform attackPosition;
    public LayerMask allLayers;
    public GameObject direction;

    private Player player;
    private PlayerMovement playerMovement;
    private float auxInputMele;

    private void Start()
    {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        auxInputMele = Input.GetAxis(inputMele);

        if (auxInputMele == 1)
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
        //Collider2D[] hitSomething = Physics2D.OverlapCircleAll(positionAttack.position, rangeAttack, allLayers);
        Collider2D hitSomething = Physics2D.OverlapCircle(attackPosition.position, player.attackRange, allLayers);

        // Enemy
        if (hitSomething.gameObject.layer == 11)
        {
            Enemy enemy = hitSomething.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(player.damage);
        }
        // Wall
        else if (hitSomething.gameObject.layer == 8)
        {

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPosition == null)
            return;

        Gizmos.DrawWireSphere(attackPosition.position, player.attackRange);
    }
}
