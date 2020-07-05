using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelShortcut : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))           // Nivel 1
            levelManager.ChangeLevel(1);    
        else if (Input.GetKeyDown(KeyCode.F2))      // Nivel 2
            levelManager.ChangeLevel(2);
        else if (Input.GetKeyDown(KeyCode.F3))      // Nivel 3
            levelManager.ChangeLevel(3);
        else if (Input.GetKeyDown(KeyCode.F4))      // Nivel 4
            levelManager.ChangeLevel(4);
        else if (Input.GetKeyDown(KeyCode.F5))      // Nivel 5
            levelManager.ChangeLevel(5);
    }
}
