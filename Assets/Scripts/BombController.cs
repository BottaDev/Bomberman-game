using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timeToExplode = 3;
    public int explosionRange = 2;
    public GameObject explosionGO;
    public LayerMask levelMask;

    private void Start()
    {
        Invoke("Explode", timeToExplode);
    }

    private void Explode()
    {
        StartCoroutine(CreateExplosions(Vector2.up));
        StartCoroutine(CreateExplosions(Vector2.right));
        StartCoroutine(CreateExplosions(Vector2.down));
        StartCoroutine(CreateExplosions(Vector2.left));

        Destroy(gameObject, 0.3f);
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i < explosionRange + 1; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), direction, out hit, i, levelMask);  // i define la distancia (en bloques) que el raycast deberia viajar

            if (!hit.collider)
                Instantiate(explosionGO, transform.position + (i * direction), explosionGO.transform.rotation);
            else
                break;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
