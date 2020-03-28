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


    // Update is called once per frame
    void Update()
    {
        
    }
}
