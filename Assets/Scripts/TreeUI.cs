using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TreeUI : MonoBehaviour
{
	public TextMeshProUGUI	text;
	private treeLevelup treeData;
	public int	resource;
	private int	amountNeeded;
	private void Awake()
	{
		treeData = FindObjectOfType<treeLevelup>();
	}
	private void Start()
	{
		text = GetComponent<TextMeshProUGUI>();
		amountNeeded = treeData.fibo[treeData.level - 1];
		text.text = 0.ToString();
	}

	private int GetResrc(int val)
	{
		if (val == 0)
			return (treeData.mineralCount);
		else if(val == 1)
			return (treeData.waterCount);
		else if (val == 2)
			return (treeData.rootCount);
		else
			return (treeData.glueCount);
	}

	private void	UpdateText()
	{
		amountNeeded = treeData.fibo[treeData.level - 1];
		int	val = GetResrc(resource);
		string msg = val + " / " + amountNeeded;
		text.text = msg;
	}

	private void Update()
	{
		UpdateText();
	}
}
