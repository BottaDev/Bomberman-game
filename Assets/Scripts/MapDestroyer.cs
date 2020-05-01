using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
	public Tile indestructibleTile;
	public Tile destructibleTile;
	public GameObject explosionGO;
	int randomProbability;
	int randomProbabilityPowerUp;
	public GameObject bombPlusGO;
	public GameObject moreRangeGO;
	public GameObject moreSpeedGO;

	private MapController mapController;
	private bool[] canExplode = { true, true, true, true };   // 0 = Up ; 1 = Right ; 2 = Down ; 3 = Left

	private void Start()
	{
		mapController = GameObject.Find("Grid Map").GetComponent<MapController>();

		if (mapController == null)
		{
			Debug.Log("Error. No se encontró el objeto 'Grid Map'");
			return;
		}
	}

	public void Explode(Vector2 worldPos, int explosionRange)
	{
		Vector3Int originCell = mapController.GetCell(worldPos);

		ExplodeCell(originCell);
		for (int i = 0; i < explosionRange; i++)
		{
			ExplodeCell(originCell + new Vector3Int(0, 1 + i, 0), canExplode[0], 0);	// Up
			ExplodeCell(originCell + new Vector3Int(1 + i, 0, 0), canExplode[1], 1);	// Right
			ExplodeCell(originCell + new Vector3Int(-1 - i, 0, 0), canExplode[2], 2);	// Down
			ExplodeCell(originCell + new Vector3Int(0, -1 - i, 0), canExplode[3], 3);	// Left
		}
	}

	private void ExplodeCell(Vector3Int cell, bool canExplode = true, int direction = -1)
	{
		if (canExplode)
		{
			Tile tile = mapController.GetTile(cell);

			if (tile == indestructibleTile && direction != -1)
			{
				this.canExplode[direction] = false;
				return;
			}
				
			if (tile == destructibleTile)
			{
				mapController.DestroyCell(cell);
				this.canExplode[direction] = false;
				DropPowerUp(cell);
			}

			Vector3 position = mapController.GetCellToWorld(cell);
			Instantiate(explosionGO, position, Quaternion.identity);
		}
	}

	// Los dropeos deberia estar en MapController
    void DropPowerUp(Vector3Int cell)
    {
		Grid grid = GameObject.Find("Grid Map").GetComponent<Grid>();

		switch (randomProbability = Random.Range(1,11))
        {
			case 6:
				Vector3 cellPosition = grid.GetCellCenterWorld(cell);
				Instantiate(bombPlusGO, cellPosition, Quaternion.identity);
				break;

			case 7:
				cellPosition = grid.GetCellCenterWorld(cell);
				Instantiate(bombPlusGO, cellPosition, Quaternion.identity);
				break;

			case 8:
				cellPosition = grid.GetCellCenterWorld(cell);
				Instantiate(bombPlusGO, cellPosition, Quaternion.identity);
				break;

			case 9:
				PowerUpType(cell);
				break;
                
			case 10:
				PowerUpType(cell);
				break;
		}
	}
    
    void PowerUpType(Vector3Int cell)
    {
        switch (randomProbabilityPowerUp = Random.Range(1,3))
        {
			case 1:
				Grid grid = GameObject.Find("Grid Map").GetComponent<Grid>();
				Vector3 cellPosition = grid.GetCellCenterWorld(cell);
				Instantiate(moreRangeGO, cellPosition, Quaternion.identity);
				break;

			case 2:
				grid = GameObject.Find("Grid Map").GetComponent<Grid>();
				cellPosition = grid.GetCellCenterWorld(cell);
				Instantiate(moreSpeedGO, cellPosition, Quaternion.identity);
				break;
        }
    }
}
