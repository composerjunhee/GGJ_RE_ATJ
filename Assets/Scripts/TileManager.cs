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
	Vector3Int localPlace;
	private bool shaking = false;
	private float time;
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

	private void	RestoreTile(TileBase tile, Vector3Int pos)
	{
		map.SetTile(pos, tile);
	}

	private void TransforTile(Vector3Int pos, float rotation)
	{
		var tileTransform = Matrix4x4.Translate(new Vector3(0, 0, 0)) * 
							Matrix4x4.Rotate(Quaternion.Euler(0, 0, rotation));
		var changeData = new TileChangeData
		{
			position = pos,
			tile = replaceTile,
			color = Color.white,
			transform = tileTransform
		};
		map.SetTile(changeData, false);
	}
	private void ShrinkTiles()
	{
		int	i  = Random.Range(0, totalTiles);
		if (tiles.ContainsKey(i))
		{
			localPlace = tiles[i];
			originalTile = map.GetTile(localPlace);
			if (map.HasTile(localPlace))
				shaking = true;
		}
	}

	private void Awake()
	{
		totalTiles = GetWorldTiles();
	}

	private void	Update()
	{
		int	timer = 2;
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int gridPosition = map.WorldToCell(mousePosition);
			change_strenght(gridPosition);
			ShrinkTiles();
		}
		if (shaking && time < timer)
		{
			time += Time.deltaTime;
			TransforTile(localPlace, Random.Range(-10f, 10f));
		}
		if (shaking && time >= timer)
		{
			TransforTile(localPlace, 0);
			RestoreTile(originalTile, localPlace);
			time = 0;
			shaking = false;
		}
	}
    
}
