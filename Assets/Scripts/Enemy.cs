using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.name == "man")
		{
			Debug.Log("Detected");
			//Add some force to the player here, when he is hit by the enemy; Also decrease the health;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
