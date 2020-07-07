using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class MapDestroyer : MonoBehaviour
{
	public Tile[] indestructibleTile;
	public Tile destructibleTile;
	public GameObject explosionGO;
	public GameObject blockSound;

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

		for (int i = 0; i < canExplode.Length; i++)
			canExplode[i] = true;
	}

	private void ExplodeCell(Vector3Int cell, bool canExplode = true, int direction = -1)
	{
		if (canExplode)
		{
			Tile tile = mapController.GetTile(cell);

			if (CheckIndestructibleTile(tile) && direction != -1)
			{
				this.canExplode[direction] = false;
				return;
			}
				
			if (tile == destructibleTile)
			{
				mapController.DestroyCell(cell);
				Instantiate(blockSound, mapController.GetCellToWorld(cell), Quaternion.identity);
				this.canExplode[direction] = false;
                if (SceneManager.GetActiveScene().buildIndex != 1)
                {
					mapController.DropPowerUp(cell);
				}
			}

			Vector3 position = mapController.GetCellToWorld(cell);
			Instantiate(explosionGO, position, Quaternion.identity);
		}
	}

    private bool CheckIndestructibleTile(Tile currentTile)
    {
		if (currentTile != null)
		{
			for (int i = 0; i < indestructibleTile.Length; i++)
			{
				if (currentTile == indestructibleTile[i])
					return true;
			}
		}

		return false;
    }
}
