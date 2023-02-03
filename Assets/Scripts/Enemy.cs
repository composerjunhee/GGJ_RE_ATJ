using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Enemy : MonoBehaviour
{
	private GameObject player;
	private Rigidbody2D rb;
	private PlayerData hp;
	[SerializeField]
	private int	damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		rb = player.GetComponent<Rigidbody2D>();
		hp = FindObjectOfType<PlayerData>();
    }	

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.name == "man")
		{
			//Reduce the health of the player HEre!!!!!
			hp.LooseHP(damage);
			rb.AddForce(transform.up * 5.0f, ForceMode2D.Impulse);
			//rb.AddForce(transform.right * -500.0f, ForceMode2D.Impulse);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
