using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineBlock : MonoBehaviour
{
    private Vector2		position;
    private float		rayDistance = 0.5f;
	public Tilemap		map;
	private Vector3Int	cellPosition;
	private TileBase	tile;
	private float		inputRawX;
	private float		inputRawY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		// Player position
		position = transform.position;
        // Get movement direction (-1 left, 1 right)
        inputRawX = Input.GetAxisRaw("Horizontal");
		// Get movement direction (-1 down, 1 up)
		inputRawY = Input.GetAxisRaw("Vertical");
        if (inputRawX != 0.0f)
        {
            // Checks if there is a tile next to the player on the horizontal axis 
            cellPosition = map.WorldToCell(position + new Vector2(0.6f * inputRawX, 0));	
			tile = map.GetTile(cellPosition);	
        }
		if (inputRawY != 0.0f)
        {
			// Checks if there is a tile next to the player on the vertical axis 
			cellPosition = map.WorldToCell(position + new Vector2(0, 1.1f * inputRawY));
			tile = map.GetTile(cellPosition);
        }
		if (tile != null)
		{
			Debug.Log("tile: " + tile);
			map.SetTile(cellPosition, null);
		}
    }
}