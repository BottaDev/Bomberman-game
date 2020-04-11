using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
	public Tile indestructibleTile;
	public Tile destructibleTile;
	public GameObject explosionGO;

	[SerializeField]
	private Tilemap tilemap;

	private bool[] canExplode = { true, true, true, true };   // 0 = Up ; 1 = Right ; 2 = Down ; 3 = Left

	public void Explode(Vector2 worldPos, int explosionRange)
	{
		tilemap = GameObject.Find("Blocks").GetComponent<Tilemap>();

		Vector3Int originCell = tilemap.WorldToCell(worldPos);

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
			Tile tile = tilemap.GetTile<Tile>(cell);

			if (tile == indestructibleTile && direction != -1)
			{
				this.canExplode[direction] = false;
				return;
			}
				
			if (tile == destructibleTile)
			{
				tilemap.SetTile(cell, null);
				this.canExplode[direction] = false;
			}

			Vector3 pos = tilemap.GetCellCenterWorld(cell);
			Instantiate(explosionGO, pos, Quaternion.identity);
		}
	}
}
