﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [Header("Player 1 Area")]
    public TextMeshProUGUI hp;
    public TextMeshProUGUI bombs;
    [Header("Player 2 Area")]
    public GameObject player2Area;
    public TextMeshProUGUI hp2;
    public TextMeshProUGUI bombs2;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        if (GameManager.instance.numberOfPlayers == 2)
            player2Area.SetActive(true);
        else
            player2Area.SetActive(false);
    }

    public void SetPlayer1HP(int hp)
    {
        this.hp.text = hp.ToString();
    }

    public void SetPlayer1Bombs(int bombs)
    {
        this.bombs.text = bombs.ToString();
    }

    public void SetPlayer2HP(int hp)
    {
        this.hp2.text = hp.ToString();
    }

    public void SetPlayer2Bombs(int bombs)
    {
        this.bombs2.text = bombs.ToString();
    }
}
