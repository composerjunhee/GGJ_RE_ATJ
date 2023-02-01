using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
	[SerializeField]
	private Tilemap map;

	private Dictionary<Vector3Int, int> strengthTiles = new Dictionary<Vector3Int, int>();
    
	private void	change_strenght(Vector3Int gridPosition)
	{
		if (!strengthTiles.ContainsKey(gridPosition))
			strengthTiles.Add(gridPosition, Random.Range(3, 7));

		int	new_strength = strengthTiles[gridPosition] - 1;
		if (new_strength <= 0)
		{
			strengthTiles.Remove(gridPosition);
			map.SetTile(gridPosition, null);
		}
		else
		{
			strengthTiles[gridPosition]--;
		}
	}

	private void	Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int gridPosition = map.WorldToCell(mousePosition);
			change_strenght(gridPosition);
		}
	}
    
}
