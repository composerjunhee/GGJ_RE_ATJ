using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
	[SerializeField]
	private Tilemap map;
	private BoundsInt bounds;
	private Dictionary<Vector3Int, int> strengthTiles = new Dictionary<Vector3Int, int>();
    private TileBase[] allTiles;
	/*private	void	Get_tiles()
	{
		bounds = map.cellBounds;
		allTiles = map.GetTilesBlock(bounds);
		for (int x = 0; x < bounds.size.x; x++)
		{
			for (int y = 0; y < bounds.size.y; y++)
			{
				TileBase tile = allTiles[x + y * bounds.size.x];
				print("tile name: " + tile.name);
			}
		}
		
	}*/
	public void	change_strenght(Vector3Int gridPosition)
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

	/*private void	ShrinkTile()
	{
		int	x = bounds.size.x;
		int	y = bounds.size.y;


		print("first tile is: " + allTiles[0]);
	}*/

	/*private void Awake()
	{
		Get_tiles();
	}*/
	private void	Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int gridPosition = map.WorldToCell(mousePosition);
			change_strenght(gridPosition);
			//ShrinkTile();
		}
	}
    
}
