using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
	private PlayerData data;
	private float timer;
	private float timerOver;
	private GameObject player;


	private void	Start()
	{
		data = FindObjectOfType<PlayerData>();
		SetMaxHealth(data.maxHP);
		player = GameObject.FindGameObjectWithTag("Player");
	}
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
		//slider.minValue = data.minHP;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

	private void HandleUnderGround()
	{
		if (player.transform.position.y < -5)
		{
			float time = 1.0f;
			timer += Time.deltaTime;
			if (timer > time)
			{
				data.hp--;
				if (data.hp < data.minHP)
					data.hp = data.minHP;
				timer = 0;
			}
		}
		else
			timer = 0;
	}
	private void HandleOverGround()
	{
		if (player.transform.position.y > -5)
		{
			float time = 1.0f;
			timerOver += Time.deltaTime;
			if (timerOver > time)
			{
				data.hp += 5;
				if (data.hp > data.maxHP)
					data.hp = data.maxHP;
				timerOver = 0;
			}
		}
		else
			timerOver = 0;
	}

	private void Update()
	{
		HandleUnderGround();
		HandleOverGround();
		SetHealth(data.hp);
		if (data.hp <= 0)
		{
			SceneManager.LoadScene("GameOver");
		}
	}
}
