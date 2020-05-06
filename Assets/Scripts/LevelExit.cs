using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();

        if (levelManager != null)
            levelManager.WinLevel();
        else
            Debug.Log("Error. No se encontró un Level Manager");
    }
}
