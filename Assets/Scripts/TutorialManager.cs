using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    PlayerInput move;
    DroppableBombController bomb;
    public Player player;
    public GameObject playerGO;
    public GameObject enemyGO;
    public GameObject portal;
    public Transform spawnPortal;
    public int enemyDead;
    public GameObject WinScreen;
    public GameObject loseScene;

    private void Start()
    {
        move = playerGO.GetComponent<PlayerInput>();
        bomb = playerGO.GetComponent<DroppableBombController>();
        bomb.enabled = false;
        player = playerGO.GetComponent<Player>();
    }

    private void Update()
    {
        if (player != null)
        {
            for (int i = 0; i < popUps.Length; i++)
            {
                if (i == popUpIndex)
                {
                    popUps[i].SetActive(true);
                }
                else
                {
                    popUps[i].SetActive(false);
                }
            }

            if (popUpIndex == 0)
            {
                if (Input.GetAxis(move.inputAxisY) > 0)
                {
                    move.moveUp = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 1)
            {
                if (Input.GetAxis(move.inputAxisY) < 0)
                {
                    move.moveDown = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 2)
            {
                if (Input.GetAxis(move.inputAxisX) > 0)
                {
                    move.moveRight = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 3)
            {
                if (Input.GetAxis(move.inputAxisX) < 0)
                {
                    move.moveLeft = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 4)
            {
                if (Input.GetAxis(bomb.inputBomb) > 0)
                {
                    bomb.enabled = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 5)
            {
                if (enemyDead >= 1)
                {
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 6)
            {

            }
        }
        else
        {
            print("No se encontro al player");
            return;
        }

    }

    public void SpawnPortal()
    {
        Instantiate(portal, spawnPortal);
    }

    public IEnumerator WinTutorial()
    {
        GameObject winTutorial = WinScreen.transform.GetChild(1).gameObject;

        winTutorial.SetActive(true);
        bool sdasafsdfa = winTutorial.activeSelf;
        yield return null;
    }
}