using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelShortcut : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(1);
        else if (Input.GetKeyDown(KeyCode.F2))
            SceneManager.LoadScene(2);
        else if (Input.GetKeyDown(KeyCode.F3))
            SceneManager.LoadScene(3);
        else if (Input.GetKeyDown(KeyCode.F4))
            SceneManager.LoadScene(4);
        else if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene(5);
    }
}
