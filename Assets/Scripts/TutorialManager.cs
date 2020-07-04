using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    
    public GameObject playerGO;
    public GameObject enemyGO;
    public GameObject portal;
    public Transform spawnPortal;
    public GameObject winScreen;
    public GameObject loseScene;
    [HideInInspector]
    public bool enemyDead;
    public TextMeshProUGUI hp;

    private Player player;
    private int popUpIndex;
    private PlayerInput move;
    private DroppableBombController bomb;

    private void Start()
    {
        move = playerGO.GetComponent<PlayerInput>();
        bomb = playerGO.GetComponent<DroppableBombController>();
        bomb.enabled = false;
        player = playerGO.GetComponent<Player>();

        if (player == null)
            Debug.Log("No se encontro al player");
    }

    private void Update()
    {
        if (player != null)
        {
            // Activa / desactiva popups
            for (int i = 0; i < popUps.Length; i++)
            {
                if (i == popUpIndex)
                    popUps[i].SetActive(true);
                else
                    popUps[i].SetActive(false);
            }

            if (popUpIndex == 0)    // Mueve arriba
            {
                if (Input.GetAxis(move.inputAxisY) > 0)
                {
                    move.moveUp = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 1)   // Mueve abajo
            {
                if (Input.GetAxis(move.inputAxisY) < 0)
                {
                    move.moveDown = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 2)   // Mueve derecha
            {
                if (Input.GetAxis(move.inputAxisX) > 0)
                {
                    move.moveRight = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 3)   // Mueve  izquierda
            {
                if (Input.GetAxis(move.inputAxisX) < 0)
                {
                    move.moveLeft = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 4)   // Coloca bomba
            {
                if (Input.GetAxis(bomb.inputBomb) > 0)
                {
                    bomb.enabled = true;
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 5)   // Escapa por portal
            {
                if (enemyDead)
                    popUpIndex++;
            }
        }
    }

    public void SetUiHp(int hp)
    {
        this.hp.text = hp.ToString();
    }

    public void SpawnPortal()
    {
        enemyDead = true;

        Instantiate(portal, spawnPortal);
    }

    public IEnumerator WinTutorial()
    {
        winScreen.SetActive(true);

        yield return null;
    }
}