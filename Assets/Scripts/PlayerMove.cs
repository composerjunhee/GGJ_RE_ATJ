using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider2D;
    private float horizontal;
    public float speed = 7f;
    public float jumpingPower = 8f;
    private bool isFacingRight = true;
    public Tilemap		map;
	private Vector3Int	cellPosition;
	private TileBase	tile;
    private Animator	animator;
    private bool        inAir = false;
    private int         landingDelay = 20; // Number of frames delay. Hacky fix for landing bug 
    public  float       airManouver = 6;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Debug.Log(boxCollider2D.bounds.center - new Vector3(0, boxCollider2D.bounds.extents.y, 0));
        if (!isFacingRight && horizontal > 0f)
            Flip();
        else if (isFacingRight && horizontal < 0f)
            Flip();
        if (Input.GetButtonDown("Jump") && IsGrounded())
            animator.SetTrigger("Jump");
        // check if landed after jump
        Landed();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (inAir)
        {
            // Check if trying to move to the opposite way of current velocity
            if (horizontal != 0 && (Mathf.Sign(rb.velocity.x) > horizontal || Mathf.Sign(rb.velocity.x) < horizontal))
                rb.velocity = new Vector2(rb.velocity.x + horizontal / airManouver, rb.velocity.y);
            // Max speed check
            else if (horizontal != 0 && Mathf.Abs(rb.velocity.x) < speed)
                rb.velocity = new Vector2(rb.velocity.x + horizontal / airManouver, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
    }

    private void Landed()
    {
        if (inAir && IsGrounded())
        {
            landingDelay--;
            if (landingDelay == 0)
            {
                animator.SetTrigger("Landed");
                inAir = false;
                landingDelay = 5;
            }
        }
    }
    
    private void Jumped()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    	animator.SetTrigger("Flying");
		inAir = true;
    }
    private bool IsGrounded()
    {
        cellPosition = map.WorldToCell(capsuleCollider2D.bounds.center - new Vector3(0, capsuleCollider2D.bounds.extents.y + 0.05f, 0));
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

