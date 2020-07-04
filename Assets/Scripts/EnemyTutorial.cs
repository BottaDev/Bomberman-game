using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTutorial : MonoBehaviour
{
    [HideInInspector]
    public TutorialManager tutorial;

    private void Start()
    {
        tutorial = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }

    private void OnDestroy()
    {
        tutorial.enemyDead++;
        tutorial.SpawnPortal();
    }
}