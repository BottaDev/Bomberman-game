using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject creditsMenu;

    public void SetPlayMenu()
    {
        creditsMenu.SetActive(false);
        playMenu.SetActive(!playMenu.activeSelf);
    }

    public void SetCreditsMenu()
    {
        playMenu.SetActive(false);
        creditsMenu.SetActive(!creditsMenu.activeSelf);
    }
}
