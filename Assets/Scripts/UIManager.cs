using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [Header("Player 1 Area")]
    public GameObject player1Area;
    public TextMeshProUGUI hp;
    [Header("Player 2 Area")]
    public GameObject player2Area;
    public TextMeshProUGUI hp2;

    [Header("Screens")]
    public GameObject youLoseScreen;
    public GameObject youWinScreen;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void SetPlayer1HP(int hp)
    {
        this.hp.text = hp.ToString();
    }

    public void SetPlayer2HP(int hp)
    {
        this.hp2.text = hp.ToString();
    }

    public void ShowFinalGui(bool win)
    {
        player1Area.SetActive(false);
        player2Area.SetActive(false);

        if (win)
            youWinScreen.SetActive(true);
        else
            youLoseScreen.SetActive(true);
    }

    public void ShowWinGame()
    {
        GameObject winGame = youWinScreen.transform.GetChild(1).gameObject;
        GameObject winLevel = youWinScreen.transform.GetChild(2).gameObject;

        winLevel.SetActive(false);
        winGame.SetActive(true);
    }
}
