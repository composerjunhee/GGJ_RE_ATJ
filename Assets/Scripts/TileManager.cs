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
	public Dictionary<int, Vector3Int> tiles;
	int	totalTiles;
	[SerializeField]
	private TileBase replaceTile;
	private TileBase originalTile;
	private int GetWorldTiles () 
	{
		int i = 0;
        tiles = new Dictionary<int, Vector3Int>();
		foreach (Vector3Int pos in map.cellBounds.allPositionsWithin)
		{
			var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

			if (!map.HasTile(localPlace))
			{
				i++;
				continue;
			}
			tiles.Add(i, localPlace);
			i++;
		}
		return (i);
	}

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

	private void ShrinkTiles()
	{
		int	i  = Random.Range(0, totalTiles);
		Vector3Int localPlace;
		if (tiles.ContainsKey(i))
		{
			localPlace = tiles[i];
			if (map.HasTile(localPlace))
			{
				originalTile = map.GetTile(localPlace);
				var tileTransform = Matrix4x4.Translate(new Vector3(0.03f, 0.03f, 0)) * 
									Matrix4x4.Rotate(Quaternion.Euler(0, 0, Random.Range(-10f, 10f)));
				var changeData = new TileChangeData
				{
					position = localPlace,
					tile = replaceTile,
					color = Color.white,
					transform = tileTransform
				};
				map.SetTile(changeData, false);
			}
		}
		
	}

	private void ResetTile(Vector3Int tilePosition)
	{

	}

	private void Awake()
	{
		totalTiles = GetWorldTiles();
	}

	private void	Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int gridPosition = map.WorldToCell(mousePosition);
			change_strenght(gridPosition);
			ShrinkTiles();
		}
	}
    
}
