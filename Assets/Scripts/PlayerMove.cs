using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider2D;
    private SpriteRenderer  spriteRenderer;
    private float horizontal;
    public float speed = 7f;
    public float jumpingPower = 8f;
    private bool isFacingRight = true;
    public Tilemap map;
    private Vector3Int cellPosition;
    private TileBase tile;
    private Animator animator;
    private bool inAir = false;
    public float airManouver = 6;
    public GameObject UiObject;
    public int level = 1;
	private float textAccumulatior = 0f;
	private float inAirAccumulatior = 0f;
	private bool showingText = false;
	// private treeLevelup treeLvlup;
    public GameObject menuSet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        UiObject.SetActive(false);
		// treeLvlup = FindObjectOfType<treeLevelup>();
		LevelUp();
    }

    void Update()
    {
        //Sub menu
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
        //Facing
        if (!isFacingRight && horizontal > 0f)
            Flip();
        else if (isFacingRight && horizontal < 0f)
            Flip();
        // Jump
        if (Input.GetButtonDown("Jump") && (IsGrounded() || IsTouchingWall()))
            animator.SetTrigger("Jump");
		if (inAir)
			inAirAccumulatior += Time.deltaTime;
        // Check if landed after jump'
        Landed();
		if (showingText)
		{
			// Remove text when 2 seconds have passed
			if (textAccumulatior > 2)
			{
				UiObject.SetActive(false);
				showingText = false;
				textAccumulatior = 0f;
			}
			textAccumulatior += Time.deltaTime;
		}
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

	private void LevelUp()
	{
		level++;
		UiObject.SetActive(true);
		showingText = true;
	}

    private void Landed()
    {
        if (inAir && IsGrounded())
        {
			if (inAirAccumulatior > 0.2)
			{
				animator.SetTrigger("Landed");
                inAir = false;
				inAirAccumulatior = 0f;
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

    private bool IsTouchingWall()
    {
        // Check left
        cellPosition = map.WorldToCell(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x + 0.05f, 0, 0));
        tile = map.GetTile(cellPosition);
        if (tile)
            return (true);
        // Check right
        cellPosition = map.WorldToCell(capsuleCollider2D.bounds.center + new Vector3(capsuleCollider2D.bounds.extents.x + 0.05f, 0, 0));
        tile = map.GetTile(cellPosition);
        if (tile)
            return (true);
        return false;
    }

    private void Flip()
    {
        spriteRenderer.flipX = isFacingRight;
        isFacingRight = !isFacingRight;
    }

    public void GameExit()
    {
        Application.Quit();
    }
}

