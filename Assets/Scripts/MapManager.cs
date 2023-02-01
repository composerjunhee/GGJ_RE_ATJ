using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
	[SerializeField]
	private Tilemap map;

	[SerializeField]
	private List<DataTile> tileDatas;

	private Dictionary<TileBase, DataTile> dataFromTiles;
    // Start is called before the first frame update

	private void Awake()
	{
		dataFromTiles = new Dictionary<TileBase, DataTile>();
		foreach(var tileData in tileDatas)
		{
			foreach(var tile in tileData.tiles)
			{
				dataFromTiles.Add(tile, tileData);
			}
		}
	}
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int gridPosition = map.WorldToCell(mouse_position);

			TileBase clickedTile = map.GetTile(gridPosition);
			bool	digable = dataFromTiles[clickedTile].digable;
			print("tile "+ clickedTile.GetHashCode() + "as " + dataFromTiles[clickedTile].digable + "strenght still");
			if (digable)
			{
				dataFromTiles[clickedTile].strenght--;
				if (dataFromTiles[clickedTile].strenght <= 0)
				{
					map.SetTile(gridPosition, null);
				}
			}
			
		}
    }
}
