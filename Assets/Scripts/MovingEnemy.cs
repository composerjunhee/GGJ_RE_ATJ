using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D collider2d;
    private Collider2D target;
    private float timerDelta = 0f;
    private int dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Debug.Log("Following: " + target.bounds.center);
            Debug.Log(Vector3.Distance(collider2d.bounds.center, target.bounds.center));
            if (Vector3.Distance(collider2d.bounds.center, target.bounds.center) > 5f)
                target = null;
            rb.velocity = target.bounds.center - collider2d.bounds.center;
        }
        else
        {
            var step =  2 * Time.deltaTime; // calculate distance to move
            if (delay(2))
            {
                dir = Random.Range(0, 5);
                Debug.Log(dir);
            }
            if (dir == 1)
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(8,-25), step);
            else if (dir == 2)
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(7,-16), step);
            else if (dir == 3)
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(-7,-16), step);
            else if (dir == 4)
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(0,-21), step);
            else
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(-7,-25), step);
            
        }
           
    }

    private bool delay(float timer)
    {
        if (timerDelta > timer)
        {
            timerDelta = 0f;
            return true;
        }
        timerDelta += Time.deltaTime;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Target: " + collision.GetComponent<Collider2D>());
        if (collision.name == "player")
            target = collision.GetComponent<Collider2D>();
        // collision.attachedRigidbody
    }   

}