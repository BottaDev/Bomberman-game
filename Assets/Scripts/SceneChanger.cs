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

        ChangeScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetMenuAudio()
    {
        AudioManager.instance.SetMenuGameMusic(true);
    }
}
