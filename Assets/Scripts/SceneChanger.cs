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

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
