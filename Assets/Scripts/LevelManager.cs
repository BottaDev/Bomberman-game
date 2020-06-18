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
    private int level;

    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;

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
            //UIManager.instance.bombs2.text = "-";
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

        AudioManager.instance.SetMenuGameMusic(true);

        ChangeLevel(0);
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

    public IEnumerator WinLevel()
    {
        UIManager.instance.ShowFinalGui(true);

        AudioManager.instance.PlayWinLoseSound(false);

        yield return new WaitForSeconds(2);

        ChangeLevel(level + 1);
    }

    private void ChangeLevel(int levelIndex)
    {
        if (levelIndex > 5)
        {
            AudioManager.instance.SetMenuGameMusic(true);
            GameManager.instance.DestroyGameManager();
            SceneManager.LoadScene(0);
        }
        else
            SceneManager.LoadScene(levelIndex);
    }
}
