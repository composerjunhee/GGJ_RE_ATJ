using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	[HideInInspector]
	public int	hp = 100;
	public int	maxHP;
	[HideInInspector]
	public int minHP = 0;
	public int	bombs = 0;
	public void LooseHP(int amount)
	{
		hp -= amount;
		Debug.Log("hp remaining: " + hp);
	}
}
