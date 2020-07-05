using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            LevelManager levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();

            if (levelManager != null)
                levelManager.StartCoroutine("WinLevel");
            else
                Debug.Log("Error. No se encontró un Level Manager");
        }
        else
        {
            TutorialManager tutorial = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();

            if (tutorial != null)
                tutorial.StartCoroutine("WinTutorial");
            else
                Debug.Log("Error. No se encontró un Tutorial Manager"); 
        }

        StopPlayerMovement();
    }

    private void StopPlayerMovement()
    {
        GameObject[] inputs = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject item in inputs)
        {
            item.GetComponent<PlayerInput>().enabled = false;
        }
    }
}
