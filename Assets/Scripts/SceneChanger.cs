using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public void SetPlayers(int numberOfPlayers)
    {
        GameManager.instance.SetPlayers(numberOfPlayers);

        AudioManager.instance.SetMenuGameMusic(false);

        if (GameManager.instance.GetDifficulty() == 1)      // Easy
            ChangeScene(2);
        else if (GameManager.instance.GetDifficulty() == 2)     // Medium
            ChangeScene(3);
        else if (GameManager.instance.GetDifficulty() == 3)     // Hard
            ChangeScene(4);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void StartTutorial()
    {
        AudioManager.instance.SetMenuGameMusic(false);

        SceneManager.LoadScene(1);
    }

    public void SetMenuAudio()
    {
        AudioManager.instance.SetMenuGameMusic(true);
    }
}
