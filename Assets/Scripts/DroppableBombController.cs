using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableBombController : MonoBehaviour
{
    public GameObject bombGO;

    private Player player;
    private float currentBombCd = 0;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        currentBombCd -= Time.deltaTime;
        if (currentBombCd <= 0)
            currentBombCd = 0;

        if (Input.GetKeyDown(KeyCode.Space) && currentBombCd <= 0)
            DropBomb();
    }

    private void DropBomb()
    {
        GameObject bomb = Instantiate(bombGO, new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), bombGO.transform.position.z), bombGO.transform.rotation);
        BombController bombController = bomb.GetComponent<BombController>();
        bombController.timeToExplode = player.bombTimeToExplode;
        bombController.explosionRange = player.bombRange;

        currentBombCd = player.bombCd;
    }
}
