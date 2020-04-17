using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    public Transform positionAttack;
    public GameObject direction;
    public float rangeAttack = 0.5f;
    public LayerMask allLayers;
    public float damage = 0.5f;
    public PlayerMovement player;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
        if (player.moveUp == true)
            direction.transform.localPosition = new Vector3(0, 0.1f, 0);
        if (player.moveDown == true)
            direction.transform.localPosition = new Vector3(0, -0.1f, 0);
        if (player.moveRight == true)
            direction.transform.localPosition = new Vector3(0.1f, 0, 0);
        if (player.moveLeft == true)
            direction.transform.localPosition = new Vector3(-0.1f, 0, 0);
    }

    void Attack()
    {
        Collider2D[] hitSomething = Physics2D.OverlapCircleAll(positionAttack.position, rangeAttack, allLayers);

        foreach(Collider2D objects in hitSomething)
        {
            Debug.Log("Le di");
            Enemy enemy;
            //MapDestroyer box;

            if (enemy = objects.gameObject.GetComponent<Enemy>())
                enemy.TakeDamage(damage);

            //else if (box = objects.gameObject.GetComponent<MapDestroyer>())
              //  box.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (positionAttack == null)
            return;

        Gizmos.DrawWireSphere(positionAttack.position, rangeAttack);
    }
}
