using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject creditsMenu;

    // Setea la cantidad de players en la partida
    public void SetPlayers(int numberOfPlayers)
    {
        GameManager.instance.SetPlayers(numberOfPlayers);

        SceneManager.LoadScene(1);
    }

    // Setea el enabled del menu PlayManu
    public void SetPlayMenu()
    {
        creditsMenu.SetActive(false);
        playMenu.SetActive(!playMenu.activeSelf);
    }

    // Setea el enabled del menu CreditsMenu
    public void SetCreditsMenu()
    {
        playMenu.SetActive(false);
        creditsMenu.SetActive(!creditsMenu.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
