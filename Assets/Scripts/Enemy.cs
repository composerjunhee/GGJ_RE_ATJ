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
	private int	damage = 60;
	private GameObject AD;
	private AudioSource audioDamage;
    // Start is called before the first frame update

	private void	Awake()
	{
		AD = GameObject.FindGameObjectWithTag("AudioDamage");
		audioDamage = AD.GetComponent<AudioSource>();
	}
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		rb = player.GetComponent<Rigidbody2D>();
		hp = FindObjectOfType<PlayerData>();
    }	

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.name == "player")
		{
			hp.LooseHP(damage);
			audioDamage.Play();
			rb.AddForce(transform.up * 5.0f, ForceMode2D.Impulse);
		}
	}
}
