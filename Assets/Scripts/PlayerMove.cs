using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private float horizontal;
    public float speed = 7f;
    public float jumpingPower = 8f;
    private bool isFacingRight = true;
    public Tilemap		map;
	private Vector3Int	cellPosition;
	private TileBase	tile;
    private Animator	animator;
    private bool        inAir = false;
    private int         landing_delay = 5; // Number of frames delay. Hacky fix for landing bug 

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Debug.Log(boxCollider2D.bounds.center - new Vector3(0, boxCollider2D.bounds.extents.y, 0));
        if (!isFacingRight && horizontal > 0f)
            Flip();
        else if (isFacingRight && horizontal < 0f)
            Flip();
        // if (Input.GetButtonDown("Jump") && !IsGrounded())
        //     rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            animator.SetTrigger("Jump");
            // rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        // landed after jump
        if (inAir && IsGrounded())
        {
            landing_delay--;
            if (landing_delay == 0)
            {
                animator.SetTrigger("Landed");
                inAir = false;
                landing_delay = 5;
            }
        }
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Jumped()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    	animator.SetTrigger("Flying");
		inAir = true;
    }
    private bool IsGrounded()
    {
        cellPosition = map.WorldToCell(boxCollider2D.bounds.center - new Vector3(0, boxCollider2D.bounds.extents.y + 0.05f, 0));
        
        tile = map.GetTile(cellPosition);
        if (tile)
            return (true);
        return false;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}

