using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject creditsMenu;
    public TextMeshProUGUI difficultyText;

    private void Start()
    {
        SetDifficultyText(GameObject.Find("GameManager").GetComponent<GameManager>().GetDifficulty());
    }

    public void SetDifficultyText(int diff)
    {
        if (diff == 1)
            difficultyText.text = GameManager.Difficulty.Easy.ToString();
        else if (diff == 2)
            difficultyText.text = GameManager.Difficulty.Medium.ToString();
        else if (diff == 3)
            difficultyText.text = GameManager.Difficulty.Hard.ToString();
    }

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
