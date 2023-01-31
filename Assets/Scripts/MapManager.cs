using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
	[SerializeField]
	private Tilemap map;

	[SerializeField]
	private List <tile_data> tileDatas;

	private Dictionary <TileBase, tile_data> dataFromTiles;
    // Start is called before the first frame update

	private void awake()
	{
		dataFromTiles = new Dictionary <TileBase, tile_data>();
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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int gridPosition = map.WorldToCell(mouse_position);

			TileBase clickedTile = map.GetTile(gridPosition);
			int	digable = dataFromTiles[clickedTile].digable;
		}
    }
}
