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
            GetLevelDifficulty(1);    
        else if (Input.GetKeyDown(KeyCode.F2))      // Nivel 2
            GetLevelDifficulty(2);
        else if (Input.GetKeyDown(KeyCode.F3))      // Nivel 3
            GetLevelDifficulty(3);
        else if (Input.GetKeyDown(KeyCode.F4))      // Nivel 4
            GetLevelDifficulty(4);
        else if (Input.GetKeyDown(KeyCode.F5))      // Nivel 5
            GetLevelDifficulty(5);
    }

    private void GetLevelDifficulty(int level)
    {
        if (level == 1)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(2);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(3);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(4);
        }
        else if (level == 2)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(5);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(6);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(7);
        }
        else if (level == 3)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(8);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(9);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(10);
        }
        else if (level == 4)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(11);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(12);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(13);
        }
        else if (level == 5)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(14);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(15);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(16);
        }
    }
}
