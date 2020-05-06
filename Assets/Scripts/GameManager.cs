using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject[] playerBaseGO = new GameObject[2];    // Prefab base de los players

    [HideInInspector]
    public bool[] playerIsDead = new bool[2];
    public int numberOfPlayers = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    // Llamado por los botones en el menu
    public void SetPlayers(int number)
    {
        numberOfPlayers = number;
    }

    public int GetNumberOfPlayers()
    {
        return numberOfPlayers;
    }

    // Se llama a esta funcion para evitar conflictors a la hora de volver al menu
    public void DestroyGameManager()
    {
        Destroy(this);
    }
}
