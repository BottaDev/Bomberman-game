using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float bombCd;
    [Range(min: 0.5f, max: 3f)]
    public float bombTimeToExplode = 3f;
    [Range(min: 2, max: 8)]
    public int bombRange = 2;   // Valor expresado en bloques del mapa
    public float speed;
    public float life = 2;
    public BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Destroy(gameObject);
            //SceneManager.LoadScene("");
        }
        StartCoroutine("SetInvunerable");
    }

    IEnumerator SetInvunerable()
    {
        boxCollider.enabled = false; 
        yield return new WaitForSeconds(2);
        boxCollider.enabled = true;
    }
}
