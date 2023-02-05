 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 public class treeLevelup : MonoBehaviour
 {
	public int waterCount;
	public int glueCount;
	public int rootCount;
	public int	mineralCount;
	public int level;

	public int[] fibo;
	private PlayerData data;
	private inventory inv;
	GameObject slotPanel;
	private GameObject tree;
	private AudioSource audioSource;
	private bool treeAlive = true;
	public float	time;
	public	int		treeLifeTime;


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
		Debug.Log("returned: " + material);
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
			Debug.Log("Entered HEre!");
			level++;
			waterCount = glueCount = rootCount = mineralCount = 0;
			float	y = tree.transform.position.y;
			float	zoomX = tree.transform.localScale.x;
			float	zoomY = tree.transform.localScale.y;
			time = 0;
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

	private int	GetTreeLifetime(int level)
	{
		if (level < 2)
			return (65);
		else if (level < 3)
			return (80);
		else if (level < 4)
			return (100);
		else if (level < 5)
			return (115);
		else if (level < 6)
			return (180);
		else if (level < 7)
			return (240);
		else
			return (300);
	}

	private void	TreeLife()
	{
		time += Time.deltaTime;
		treeLifeTime = GetTreeLifetime(level);
		if (time > treeLifeTime)
			SceneManager.LoadScene("GameOver");
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (data.items == 4 && col.name == "player")
		{
			for(int i = 0; i < inv.slots.Count; i++)
				GiveMaterial(inv.slots[i].item, i);
		}
	}

	private void	Update()
	{
		TreeLife();
	}
}
