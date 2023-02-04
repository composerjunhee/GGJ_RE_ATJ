 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

 public class treeLevelup : MonoBehaviour
 {
	public int waterCount;
	public int glueCount;
	public int rootCount;
	public int	mineralCount;
	public int level;

	private int[] fibo;
	private PlayerData data;
	private inventory inv;
	GameObject slotPanel;
	private GameObject tree;
	private AudioSource audioSource;



	private void Awake()
	{
		data = FindObjectOfType<PlayerData>();
		inv = FindObjectOfType<inventory>();
		GenerateFibo(10);
		slotPanel = GameObject.Find("Panel");
		tree = GameObject.FindGameObjectWithTag("Tree");
		audioSource = GetComponent<AudioSource>();
	}

     private void Start()
     {
         level = 1;
     }

	private void GenerateFibo(int length)
	{
		fibo = new int[length];
		fibo[0] = 1;
		fibo[1] = 1;

		for (int i = 2; i < length; i++)
			fibo[i] = fibo[i - 1] + fibo[i - 2];
	}
	public void GiveMaterial(string material, int i)
	{
		if (material == "water" || material == "water(Clone)")
			waterCount++;
		else if (material == "glue" || material == "glue(Clone)")
			glueCount++;
		else if (material == "Wood" || material == "Wood(Clone)")
			rootCount++;
		else if (material == "mineral" || material == "mineral(Clone)")
			mineralCount++;

		if (waterCount >= fibo[level - 1] && glueCount >= fibo[level - 1] && rootCount >= fibo[level - 1] && mineralCount >= fibo[level - 1])
		{
			level++;
			waterCount = glueCount = rootCount = mineralCount = 0;
			float	y = tree.transform.position.y;
			float	zoomX = tree.transform.localScale.x;
			float	zoomY = tree.transform.localScale.y;
			tree.transform.localScale = new Vector2(zoomX + 0.2f, zoomY + 0.2f);
			tree.transform.position = new Vector2(tree.transform.position.x, y + 0.2f);
			if (!audioSource)
				Debug.Log("Audio source not available");
			else
				audioSource.Play();
		}
		data.items--;
		inv.slots[i].isEmpty = true;
		inv.slots[i].item = null;
		Destroy(inv.slots[i].itemObj);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (data.items == 4 && col.name == "player")
		{
			for(int i = 0; i < inv.slots.Count; i++)
				GiveMaterial(inv.slots[i].item, i);
		}
	}
 }