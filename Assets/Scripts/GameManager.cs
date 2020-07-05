using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject[] playerBaseGO = new GameObject[2];    // Prefab base de los players
    [SerializeField]
    private Difficulty currentDifficulty = Difficulty.Easy;

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

    // Llamado por los botones de jugar en el menu
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

    // Llamado por los botones de dificultad en el menu
    public void SetDifficulty(int diff)
    {
        if (diff == 1)
            currentDifficulty = Difficulty.Easy;
        else if (diff == 2)
            currentDifficulty = Difficulty.Medium;
        else if (diff == 3)
            currentDifficulty = Difficulty.Hard;
        else
            Debug.LogWarning("Difficultad ingresada incorrecta");
    }

    public int GetDifficulty()
    {
        return (int) currentDifficulty;
    }

    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3,
    }
}
