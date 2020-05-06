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
            SpawnPlayer();
        else if ((GameManager.instance.numberOfPlayers == 2))
            SpawnPlayers();
    }

    private void SpawnPlayer()
    {
        int randomNum = Random.Range(0, 2);

        if (level == 1)
        {
            if (randomNum == 0)
                InstantiatePlayer(GameManager.instance.playerBaseGO[0], spawnPositions[0]);
            else
                InstantiatePlayer(GameManager.instance.playerBaseGO[0], spawnPositions[1]);
        }
        else
        {
            if (randomNum == 0)
                InstantiatePlayer(GameManager.instance.currentPlayerGO[0], spawnPositions[0], GameManager.instance.playerIsDead[0]);
            else
                InstantiatePlayer(GameManager.instance.currentPlayerGO[0], spawnPositions[1], GameManager.instance.playerIsDead[0]);
        }
    }

    private void SpawnPlayers()
    {
        int level = SceneManager.GetActiveScene().buildIndex;

        int randomNum = Random.Range(0, 2);

        // Primer nivel, los players no tiene buffs
        if (level == 1)
        {
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
        else
        {
            if (randomNum == 0)
            {
                InstantiatePlayer(GameManager.instance.currentPlayerGO[0], spawnPositions[0], GameManager.instance.playerIsDead[0]);
                InstantiatePlayer(GameManager.instance.currentPlayerGO[1], spawnPositions[1], GameManager.instance.playerIsDead[1]);
            }
            else
            {
                InstantiatePlayer(GameManager.instance.currentPlayerGO[0], spawnPositions[1], GameManager.instance.playerIsDead[0]);
                InstantiatePlayer(GameManager.instance.currentPlayerGO[1], spawnPositions[0], GameManager.instance.playerIsDead[1]);
            }
        }
    }

    private void InstantiatePlayer(GameObject player, Transform spawn, bool isDead = false)
    {
        // Si el player murio en el anterior nivel, resetea la cantidad de vidas y la cantida de bombas
        if (isDead)
        {
            Player p = Instantiate(player, spawn.position, spawn.rotation).GetComponent<Player>();
            p.life = 2;
            p.bombStack = 3;
        }
        else
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
        yield return new WaitForSeconds(2);

        Debug.Log("Derrota");
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

    public void WinLevel(GameObject playerGO)
    {
        if (numberOfPlayers == 1)
            GameManager.instance.SetCurrentPlayerGO(Player.PlayerNum.Player1, playerGO);
        else if (numberOfPlayers == 2)
        {
            Player playerEntity = playerGO.GetComponent<Player>();
            GameManager.instance.SetCurrentPlayerGO(playerEntity.playerNum, playerGO);

            // Se destruye el objeto para no tener problemas en la busqueda del otro player
            Destroy(playerGO);

            GameObject secondPlayerGO = GameObject.FindGameObjectWithTag("Player");

            if (secondPlayerGO != null)
            {
                Player playerEntity2 = secondPlayerGO.GetComponent<Player>();
                GameManager.instance.SetCurrentPlayerGO(playerEntity2.playerNum, secondPlayerGO);
            }
        }

        ChangeLevel(level + 1);
    }

    private void ChangeLevel(int levelIndex)
    {
        if (levelIndex > 2)
        {
            GameManager.instance.DestroyGameManager();
            SceneManager.LoadScene(0);
        }
        else
            SceneManager.LoadScene(levelIndex);
    }
}
