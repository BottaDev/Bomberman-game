using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float duration = 0.5f;

    void Start()
    {
        Destroy(gameObject,duration);   
    }
}
