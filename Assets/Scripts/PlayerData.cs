using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	public int	hp = 100;

	public void LooseHP(int amount)
	{
		hp -= amount;
		Debug.Log("hp remaining: " + hp);
	}
}
