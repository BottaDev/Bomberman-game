using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timeToExplode = 3;
    public int explosionRange = 2;
    public GameObject explosionGO;
    public Animator animator;

    private CircleCollider2D bombCollider;
    private bool exploded = false;

    private void Awake()
    {
        bombCollider = GetComponent<CircleCollider2D>();
        bombCollider.enabled = false;

        StartCoroutine("ActivateCollider");
    }

    private IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(0.5f);
        bombCollider.enabled = true;
        yield return null;
    }

    public IEnumerator Explode(bool hasTime = true)
    {
        if (hasTime)
            yield return new WaitForSeconds(timeToExplode);

        GetComponent<MapDestroyer>().Explode(transform.position, explosionRange);

        exploded = true;

        Destroy(gameObject, 0.3f);

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!exploded && collision.gameObject.layer == 10)
        {
            CancelInvoke("Explode");
            StartCoroutine("Explode", false);
        }
    }

}
