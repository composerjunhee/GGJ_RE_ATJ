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
    private CapsuleCollider2D capsuleCollider2D;
    public	GameObject	TileHighlight;
    private GameObject	highlighted;

    private PlayerMove pmove;

    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        pmove = FindObjectOfType<PlayerMove>();
    }

    private TileBase targetTile()
    {
        position = transform.position;
        if (mineDirectionX != 0.0f)
        {
            // Checks if there is a tile next to the player on the horizontal axis
            cellPosition = map.WorldToCell(capsuleCollider2D.bounds.center - new Vector3((capsuleCollider2D.bounds.extents.x + 0.6f) * -mineDirectionX, 0, 0));	
            tile = map.GetTile(cellPosition);	
        }
        if (mineDirectionY != 0.0f)
        {
            // Checks if there is a tile next to the player on the vertical axis 
			cellPosition = map.WorldToCell(capsuleCollider2D.bounds.center - new Vector3(0, (capsuleCollider2D.bounds.extents.y + 0.6f) * -mineDirectionY, 0));	
            tile = map.GetTile(cellPosition);
        }
        return (tile);
    }

    private void highlightTargetTile(TileBase tile)
    {
        Destroy(highlighted);
        if (tile)
            highlighted = Instantiate(TileHighlight, map.GetCellCenterWorld(cellPosition), Quaternion.Euler(0, 0, 0));
    }

	public void finnishMining()
	{
		mining = false;
        // Player position
        position = transform.position;
        tile = targetTile();
        // If there is a tile mine it off
        if (tile != null)
        {
            Debug.Log("Mined: " + tile);
            tileManager.change_strenght(cellPosition);
        }
		animator.SetBool("Mining", false);
	}

	private void CancelMining()
	{
		animator.SetBool("Mining", false);
		mining = false;
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
            highlightTargetTile(targetTile());
        }
        if (Input.GetButton("Fire1") && mining == false && !pmove.IsInAir())
        {
			Debug.Log("Started mining...");
			mining = true;
			animator.SetBool("Mining", true);
        }
    }
}