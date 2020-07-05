using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public Transform[] spawnPositions = new Transform[2];
    public GameObject leveExitGO;

    [SerializeField]
    private int totalEnemies;
    [SerializeField]
    private int enemyDeathCount = 0;
    private int playerDeathCount = 0;
    private int numberOfPlayers;
    private int currentLevelIndex;

    private void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        totalEnemies = FindObjectsOfType<Enemy>().Length;

        numberOfPlayers = GameManager.instance.GetNumberOfPlayers();

        if (GameManager.instance.numberOfPlayers == 0)
        {
            Debug.LogError("Error. No hay jugadores en la partida");
            return;
        }
        else if (GameManager.instance.numberOfPlayers == 1)
        {
            SpawnPlayer();
            UIManager.instance.hp2.text = "-";
        }
        else if ((GameManager.instance.numberOfPlayers == 2))
            SpawnPlayers();
    }

    private void SpawnPlayer()
    {
        int randomNum = Random.Range(0, 2);

        if (randomNum == 0)
            InstantiatePlayer(GameManager.instance.playerBaseGO[0], spawnPositions[0]);
        else
            InstantiatePlayer(GameManager.instance.playerBaseGO[0], spawnPositions[1]);

    }

    private void SpawnPlayers()
    {
        int randomNum = Random.Range(0, 2);

        if (randomNum == 0)
        {
            InstantiatePlayer(GameManager.instance.playerBaseGO[0], spawnPositions[0]);
            InstantiatePlayer(GameManager.instance.playerBaseGO[1], spawnPositions[1]);
        }
        else
        {
            InstantiatePlayer(GameManager.instance.playerBaseGO[0], spawnPositions[1]);
            InstantiatePlayer(GameManager.instance.playerBaseGO[1], spawnPositions[0]);
        }
    }

    private void InstantiatePlayer(GameObject player, Transform spawn, bool isDead = false)
    {
        Instantiate(player, spawn.position, spawn.rotation);
    }

    public void AddEnemyDeath()
    {
        enemyDeathCount++;

        if (enemyDeathCount == totalEnemies)
            SpawnExit();
    }

    public void CheckLoseGame()
    {
        playerDeathCount++;

        if (playerDeathCount == numberOfPlayers)
            StartCoroutine("LoseLevel");
    }

    private IEnumerator LoseLevel()
    {
        UIManager.instance.ShowFinalGui(false);

        AudioManager.instance.PlayWinLoseSound(true);

        yield return new WaitForSeconds(2);
    }

    private void SpawnExit()
    {
        Tilemap tilemap;
        tilemap = GameObject.Find("Blocks").GetComponent<Tilemap>();

        bool canSpawn;

        do
        {
            Vector2 randomInSquare = new Vector2();

            randomInSquare.y = Random.Range(-6.0f + -2.5f, 6.0f + -2.5f); 
            randomInSquare.x = Random.Range(-6f + -1.5f, 6f + -2.5f);

            Vector3Int cell = tilemap.WorldToCell(randomInSquare);
            Tile tile = tilemap.GetTile<Tile>(cell);

            if (tile == null)
            {
                Instantiate(leveExitGO, tilemap.GetCellCenterWorld(cell), Quaternion.identity);
                canSpawn = true;
            }
            else
                canSpawn = false;

        } while (canSpawn == false);
    }

    // Llamado por el portal
    public IEnumerator WinLevel()
    {
        AudioManager.instance.PlayWinLoseSound(false);

        UIManager.instance.ShowFinalGui(true);

        // Si es el nivel 5...
        if (currentLevelIndex == 14 || currentLevelIndex == 15 || currentLevelIndex == 16)
            UIManager.instance.ShowWinGame();

        yield return null;
    }

    // Llamado por los botonos de siguiente nivel y por los shortcuts
    public void ChangeLevel(int level)
    {
        AudioManager.instance.SetMenuGameMusic(false);

        if (level == 1)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(2);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(3);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(4);
        }
        else if (level == 2)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(5);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(6);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(7);
        }
        else if (level == 3)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(8);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(9);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(10);
        }
        else if (level == 4)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(11);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(12);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(13);
        }
        else if (level == 5)
        {
            if (GameManager.instance.GetDifficulty() == 1)          // Easy
                SceneManager.LoadScene(14);
            else if (GameManager.instance.GetDifficulty() == 2)     // Medium
                SceneManager.LoadScene(15);
            else if (GameManager.instance.GetDifficulty() == 3)     // Hard
                SceneManager.LoadScene(16);
        }

        /*if ((level + 1) > 5)
        {
            AudioManager.instance.SetMenuGameMusic(true);
            GameManager.instance.DestroyGameManager();
        }
        else
        {
            AudioManager.instance.SetMenuGameMusic(false);
            SceneManager.LoadScene(level + 1);
        }*/
    }

    public void RetryLevel()
    {
        AudioManager.instance.SetMenuGameMusic(false);
        SceneManager.LoadScene(currentLevelIndex);
    }
}
