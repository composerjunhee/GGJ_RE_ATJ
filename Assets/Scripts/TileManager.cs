using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
	[SerializeField]
	private Tilemap map;
	private BoundsInt bounds;
	private Dictionary<Vector3Int, int> strengthTiles = new Dictionary<Vector3Int, int>();
	public Dictionary<int, Vector3Int> tiles;
	int	totalTiles;
	[SerializeField]
	private TileBase	replaceTile;
	private TileBase	originalTile;
	[SerializeField]
	private	List<TileBase>	anim_worm;
	Vector3Int localPlace;
	private Vector3Int enemy1Place;
	private bool shaking = false;
	private float time;
	private bool animating = false;
	[SerializeField]
	private int	percent = 80;
	public	GameObject	wood;
	public	GameObject	mineral;
	public GameObject	water;
	public GameObject	glue;
	private	GameObject	player;
	[SerializeField]
	private	GameObject enemy;
	private GameObject newEnemyBody;
	private PlayerData data;
	private AudioSource[] audioSource;
	private treeLevelup treeInfo;

	private int GetWorldTiles () 
	{
		int i = 0;
        tiles = new Dictionary<int, Vector3Int>();
		foreach (Vector3Int pos in map.cellBounds.allPositionsWithin)
		{
			var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

			if (!map.HasTile(localPlace))
				continue;
			tiles.Add(i, localPlace);
			i++;
		}
		return (i);
	}

	private void	GetReward(Vector3Int gridPosition)
	{
		Vector3 spawnPosition;
		spawnPosition.x = gridPosition.x + 0.5f;
		spawnPosition.y = gridPosition.y + 0.5f;
		spawnPosition.z = gridPosition.z;
		int	rand = Random.Range(0, 99);
		
		if (rand < percent)
		{
			GameObject reward;
			int	randItem = Random.Range(0,99);
			if (randItem < 25)
				reward = Instantiate(wood, spawnPosition, Quaternion.Euler(0, 0, 0));
			else if (randItem < 50)
				reward = Instantiate(glue, spawnPosition, Quaternion.Euler(0, 0, 0));
			else if (randItem < 75)
				reward = Instantiate(mineral, spawnPosition, Quaternion.Euler(0, 0, 0));
			else
				reward = Instantiate(water, spawnPosition, Quaternion.Euler(0, 0, 0));
		}
	}

	public void	change_strenght(Vector3Int gridPosition)
	{
		if (!strengthTiles.ContainsKey(gridPosition))
			strengthTiles.Add(gridPosition, Random.Range(1, 3));

		int	new_strength = strengthTiles[gridPosition] - 1;
		if (new_strength <= 0)
		{
			strengthTiles.Remove(gridPosition);
			map.SetTile(gridPosition, null);
			GetReward(gridPosition);
		}
		else
		{
			strengthTiles[gridPosition]--;
		}
		if (!audioSource[0])
			Debug.Log("Audio source from mining not available");
		else
			audioSource[0].Play();
	}

	private void	RestoreTile(TileBase tile, Vector3Int pos)
	{
		map.SetTile(pos, tile);
	}

	private void TransforTile(Vector3Int pos, float rotation)
	{
		var tileTransform = Matrix4x4.Translate(new Vector3(0, 0, 0)) * 
							Matrix4x4.Rotate(Quaternion.Euler(0, 0, rotation));
		var changeData = new TileChangeData
		{
			position = pos,
			tile = replaceTile,
			color = Color.white,
			transform = tileTransform
		};
		map.SetTile(changeData, false);
	}

	private float	GetEnemyRange()
	{
		float range;
		if (treeInfo.level < 2)
			range = 10.0f;
		else if (treeInfo.level < 3)
			range = 8.0f;
		else if(treeInfo.level < 4)
			range = 6.0f;
		else if (treeInfo.level < 5)
			range = 4.0f;
		else
			range = 3.0f;
		return (range);
	}

	private void Awake()
	{
		totalTiles = GetWorldTiles();
		player = GameObject.FindGameObjectWithTag("Player");
		data = FindObjectOfType<PlayerData>();
		audioSource = GetComponents<AudioSource>();
		treeInfo = FindObjectOfType<treeLevelup>();
	}

	private void	DestroyEnemy()
	{
		Destroy(newEnemyBody);
	}

	public int GetWormAnim()
	{
		int i;

		i = Random.Range(0, anim_worm.Count);
		return (i);
	}

	private void EnemyAnimation()
	{
		Vector3 pos = enemy1Place;
		pos.x += 0.5f;
		pos.y += 0.5f;
		TransforTile(enemy1Place, 0);
		map.SetTile(enemy1Place, anim_worm[GetWormAnim()]);
		newEnemyBody = Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0));
	}

	private void ShrinkTiles()
	{
		float range = GetEnemyRange();
		Vector3 playerPosition = player.transform.position;
		playerPosition.x += Random.Range(-range, range + 1);
		playerPosition.y += Random.Range(-range, range + 1);
		Vector3Int r_pos = Vector3Int.RoundToInt(playerPosition);
		bool contain = tiles.ContainsValue(r_pos);
		
		if (contain)
		{
			enemy1Place = r_pos;
			originalTile = map.GetTile(enemy1Place);
			if (map.HasTile(enemy1Place))
			{
				shaking = true;
				audioSource[1].Play();
			}
		}
	}

	private void HandleShaking()
	{
		int	timer = 2;
		int	anim_time = 4;
		if (!shaking)
			ShrinkTiles();
		if (shaking && time < timer)
		{
			time += Time.deltaTime;
			TransforTile(enemy1Place, Random.Range(-10f, 10f));
			animating = true;
		}
		if (shaking && time >= timer && time < anim_time)
		{
			time += Time.deltaTime;
			if (animating)
				EnemyAnimation();
			animating = false;
		}
		if (shaking && time >= anim_time)
		{
			RestoreTile(originalTile, enemy1Place);
			time = 0;
			if (shaking)
				DestroyEnemy();
			shaking = false;
			animating = false;
		}
	}

	private void	Update()
	{
		HandleShaking();
	}
}
