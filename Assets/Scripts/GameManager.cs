using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject[] playerBaseGO = new GameObject[2];    // Prefab base de los players
    public GameObject[] currentPlayerGO = new GameObject[2];  // Prefab con buffs del player los players

    [HideInInspector]
    public bool[] playerIsDead = new bool[2];
    public int numberOfPlayers = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void AddPlayerDeath(Player.PlayerNum playerNum, GameObject playerGO)
    {
        if (playerNum == Player.PlayerNum.Player1)
        {
            playerIsDead[0] = true;
            currentPlayerGO[0] = playerGO;
        }
        else
        {
            playerIsDead[1] = true;
            currentPlayerGO[1] = playerGO;
        }
    }
}
