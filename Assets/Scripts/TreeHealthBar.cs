using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TreeHealthBar : MonoBehaviour
{
	public Image healthBarImg;
	private treeLevelup treeInfo;
	[SerializeField]
	private float percent;
	float time;
	private void Awake()
	{
		treeInfo = FindObjectOfType<treeLevelup>();
	}

	private void Start()
	{
		percent = treeInfo.treeLifeTime;
	}

	private void	UpdateBar()
	{
		percent = Mathf.Clamp(time / treeInfo.treeLifeTime, 0, 1f);
		healthBarImg.fillAmount = percent;
	}

    // Update is called once per frame
    void Update()
    {
		time = treeInfo.time;
        UpdateBar();
    }
}
