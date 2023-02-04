using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public GameObject slotItem;
	private PlayerData data;
	private AudioSource audioSource;
	private GameObject player;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	private void Start()
	{
		data = FindObjectOfType<PlayerData>();
		audioSource = player.GetComponent<AudioSource>();
	}

	private string	GetItemName(string name)
	{
		Debug.Log("picked: " + name);
		if (name == "water" || name == "water(Clone)")
			return ("water");
		else if (name == "glue" || name == "glue(Clone)")
			return ("glue");
		else if (name == "Wood" || name == "Wood(Clone)")
			return ("Wood");
		else
			return ("mineral");
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            inventory inven = collision.GetComponent<inventory>();
            for (int i = 0; i < inven.slots.Count; i++)
            {
                if(inven.slots[i].isEmpty)
                {
                    inven.slots[i].itemObj = Instantiate(slotItem, inven.slots[i].slotObj.transform, false);
					data.items++;
                    inven.slots[i].isEmpty = false;
					inven.slots[i].item = GetItemName(this.gameObject.name);
					audioSource.Play();
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
