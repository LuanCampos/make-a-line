using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
	private Transform player;
	private float maxPlayerPos;
	private float minPlayerPos;
	private float timer;
	private float score;
	private float bonus;
	private TextMeshProUGUI timerText;
	private TextMeshProUGUI scoreText;
	
	void Awake()
	{
		//player
		//timerText
		//scoreText
		//timer
	}
	
	void Update()
    {
		CheckGameOver();
		CheckAddScore();
		CountTime();
		CountScore();		
    }
	
	private void CheckGameOver()
	{
		if (player.position.y < -12 || timer <= 0)
		{
			Time.timeScale = 0f;
			//gameOverMenuUI.SetActive(true);
		}
	}
	
	private void CheckAddScore()
	{
		if (player.position.x > maxPlayerPos)
		{
			maxPlayerPos = player.position.x;
		}
		
		if (player.position.x < minPlayerPos)
		{
			minPlayerPos = player.position.x;
		}
	}
	
	private void CountTime()
	{
		timer -= Time.deltaTime;
		timerText.text = timer.ToString("#.");
	}
	
	private void CountScore()
	{
		score = (60 - timer + ((maxPlayerPos + minPlayerPos) * 3)) * 3 + bonus;
		scoreText.text = score.ToString("#.");
	}
	
}
