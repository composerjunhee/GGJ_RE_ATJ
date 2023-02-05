using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D collider2d;
    private Collider2D target;
    private Animator anim;
    private SpriteRenderer  spriteRenderer;
    private PlayerData hp;
    private float dirDelta = 0f;
    private float dmgDelta = 0f;
    private int dir = 1;
    private float step;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = FindObjectOfType<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        step =  2 * Time.deltaTime;
        if (target)
        {
            if (Vector3.Distance(collider2d.bounds.center, target.bounds.center) > 5f)
                target = null;
            if (Vector3.Distance(collider2d.bounds.center, target.bounds.center) < 1f)
            {
                anim.SetTrigger("Attack");
                if (delayDmg(0.1f))
                    hp.LooseHP(20);
            }
            Move(transform.position, new Vector2 (target.bounds.center.x, target.bounds.center.y), step * 0.8f);
        }
        else
        {
            if (delayDir(2))
            {
                dir = Random.Range(0, 5);
                Debug.Log(dir);
            }
            if (dir == 1)
                Move(transform.position, new Vector2(8,-25), step);
            else if (dir == 2)
                Move(transform.position, new Vector2(7,-16), step);
            else if (dir == 3)
                Move(transform.position, new Vector2(-7,-16), step);
            else if (dir == 4)
                Move(transform.position, new Vector2(0,-21), step);
            else
                Move(transform.position, new Vector2(-7,-25), step);
        }
    }

    private void Move(Vector3 current, Vector2 target, float step)
    {
        if (!isFacingRight && current.x - target.x < 0f)
            Flip();
        else if (isFacingRight && current.x - target.x > 0f)
            Flip();
        transform.position = Vector3.MoveTowards(current, target, step);
    }

    private void Flip()
    {
        spriteRenderer.flipX = isFacingRight;
        isFacingRight = !isFacingRight;
    }

    private bool delayDir(float timer)
    {
        if (dirDelta > timer)
        {
            dirDelta = 0f;
            return true;
        }
        dirDelta += Time.deltaTime;
        return false;
    }
    private bool delayDmg(float timer)
    {
        if (dmgDelta > timer)
        {
            dmgDelta = 0f;
            return true;
        }
        dmgDelta += Time.deltaTime;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "player")
            target = collision.GetComponent<Collider2D>();
    }   

}