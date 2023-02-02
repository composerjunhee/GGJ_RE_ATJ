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
	private TileBase	replaceTile;
	private TileBase	originalTile;
	[SerializeField]
	private	TileBase	anim_worm;
	Vector3Int localPlace;
	private bool shaking = false;
	private float time;
	private bool animating = false;
	[SerializeField]
	private int	percent = 80;
	public	GameObject	wood;
	public	GameObject	mineral;
	public GameObject	water;
	public GameObject	glue;
	private int GetWorldTiles () 
	{
		int i = 0;
        tiles = new Dictionary<int, Vector3Int>();
		foreach (Vector3Int pos in map.cellBounds.allPositionsWithin)
		{
			var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

			if (!map.HasTile(localPlace))
				continue;
			tiles.Add(i, localPlace);
			i++;
		}
		return (i);
	}

	private void	GetReward(Vector3Int gridPosition)
	{
		int	rand = Random.Range(0, 99);
		if (rand < percent)
		{
			GameObject reward;
			int	randItem = Random.Range(0,99);
			if (randItem < 25)
				reward = Instantiate(wood, gridPosition, Quaternion.Euler(0, 0, 0));
			else if (randItem < 50)
				reward = Instantiate(water, gridPosition, Quaternion.Euler(0, 0, 0));
			else if (randItem < 75)
				reward = Instantiate(mineral, gridPosition, Quaternion.Euler(0, 0, 0));
			else
				reward = Instantiate(water, gridPosition, Quaternion.Euler(0, 0, 0));
			//Rigidbody2D rBody = reward.AddComponent<Rigidbody2D>();
			//rBody.AddForce(Vector2.up * 3);
			print("rand: "+ rand + " randItem: " + randItem);
		}
	}

	public void	change_strenght(Vector3Int gridPosition)
	{
		if (!strengthTiles.ContainsKey(gridPosition))
			strengthTiles.Add(gridPosition, Random.Range(1, 3));

		int	new_strength = strengthTiles[gridPosition] - 1;
		if (new_strength <= 0)
		{
			strengthTiles.Remove(gridPosition);
			map.SetTile(gridPosition, null);
			GetReward(gridPosition);
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

	private void HandleShaking()
	{
		int	timer = 2;
		int	anim_time = 4;
		if (!shaking)
			ShrinkTiles();
		if (shaking && time < timer)
		{
			time += Time.deltaTime;
			TransforTile(localPlace, Random.Range(-10f, 10f));
			animating = true;
		}
		if (shaking && time >= timer && time < anim_time)
		{
			time += Time.deltaTime;
			if (animating)
			{
				TransforTile(localPlace, 0);
				map.SetTile(localPlace, anim_worm);
			}
			animating = false;
		}
		if (shaking && time >= anim_time)
		{
			RestoreTile(originalTile, localPlace);
			time = 0;
			shaking = false;
			animating = false;
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
		HandleShaking();
	}
}
