using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineBlock : MonoBehaviour
{
    private Vector2		position;
	public Tilemap		map;
	private Vector3Int	cellPosition;
	private TileBase	tile;
	private float		inputRawX;
	private float		inputRawY;
    private float		mineDirectionX;
	private float		mineDirectionY;
    private Animator	animator;
	private bool		mining = false;
   	public TileManager  tileManager;

    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();
    }

	public void finnishMining()
	{
		mining = false;
        // Player position
        position = transform.position;
        if (mineDirectionX != 0.0f)
        {
            // Checks if there is a tile next to the player on the horizontal axis 
            cellPosition = map.WorldToCell(position + new Vector2(0.6f * mineDirectionX, 0));	
            tile = map.GetTile(cellPosition);	
        }
        if (mineDirectionY != 0.0f)
        {
            // Checks if there is a tile next to the player on the vertical axis 
            cellPosition = map.WorldToCell(position + new Vector2(0, 1.1f * mineDirectionY));
            tile = map.GetTile(cellPosition);
        }
        // If there is a tile mine it off
        if (tile != null)
        {
            Debug.Log("tile: " + tile);
            tileManager.change_strenght(cellPosition);
        }
		animator.SetBool("Mining", mining);
	}

    // Update is called once per frame
    void Update()
    {
        if (mining == false)
        {
            // Get movement direction (-1 left, 1 right)
            inputRawX = Input.GetAxisRaw("Horizontal");
            // Get movement direction (-1 down, 1 up)
            inputRawY = Input.GetAxisRaw("Vertical");
            if (inputRawX != 0.0f)
            {
                mineDirectionX = inputRawX;
                mineDirectionY = 0f;
            }
            if (inputRawY != 0.0f)
            {
                mineDirectionY = inputRawY;
                mineDirectionX = 0f;
            }
        }
        if (Input.GetButton("Fire1") && mining == false)
        {
			Debug.Log("Mining...");
			mining = true;
			animator.SetBool("Mining", mining);
        }
    }
}