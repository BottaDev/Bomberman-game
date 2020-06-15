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
    //public TextMeshProUGUI bombs;
    [Header("Player 2 Area")]
    public GameObject player2Area;
    public TextMeshProUGUI hp2;
    //public TextMeshProUGUI bombs2;

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

    //public void SetPlayer1Bombs(int bombs)
    //{
    //    this.bombs.text = bombs.ToString();
    //}

    public void SetPlayer2HP(int hp)
    {
        this.hp2.text = hp.ToString();
    }

    //public void SetPlayer2Bombs(int bombs)
    //{
    //    this.bombs2.text = bombs.ToString();
    //}

    public void ShowFinalGui(bool win)
    {
        player1Area.SetActive(false);
        player2Area.SetActive(false);

        if (win)
            youWinScreen.SetActive(true);
        else
            youLoseScreen.SetActive(true);
    }
}
