using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timeToExplode = 3;
    public int explosionRange = 2;
    public GameObject explosionGO;

    private CircleCollider2D collider;
    private bool exploded = false;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;

        StartCoroutine("ActivateCollider");
    }

    private IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
        yield return null;
    }

    public IEnumerator Explode(bool hasTime = true)
    {
        if (hasTime)
            yield return new WaitForSeconds(timeToExplode);

        FindObjectOfType<MapDestroyer>().Explode(transform.position, explosionRange);

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
