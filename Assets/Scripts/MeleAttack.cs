using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    public Transform positionAttack;
    public float rangeAttack = 0.5f;
    public LayerMask allLayers;
    public float damage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(positionAttack.position, rangeAttack, allLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Le di");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (positionAttack == null)
            return;

        Gizmos.DrawWireSphere(positionAttack.position, rangeAttack);
    }
}
