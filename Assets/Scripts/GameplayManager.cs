using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
	private Transform ball;
	private float maxballPos;
	private float minballPos;
	private float timer;
	private float score;
	private float bonus;
	private TextMeshProUGUI timerText;
	private TextMeshProUGUI scoreText;
	private UIController uiController;
	
	void Awake()
	{
		ball = GameObject.Find("Ball").transform;
		timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();
		scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
		timer = 60.3f;
		uiController = GameObject.Find("UI Controller").GetComponent<UIController>();
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
		if (ball.position.y < -12 || timer <= 0)
		{
			uiController.ShowGameOverPanel();
		}
	}
	
	private void CheckAddScore()
	{
		if (ball.position.x > maxballPos)
		{
			maxballPos = ball.position.x;
		}
		
		if (ball.position.x < minballPos)
		{
			minballPos = ball.position.x;
		}
	}
	
	private void CountTime()
	{
		timer -= Time.deltaTime;
		timerText.text = timer.ToString("#.");
	}
	
	private void CountScore()
	{
		score = (60 - timer + ((maxballPos - minballPos) * 3)) * 3 + bonus;
		scoreText.text = score.ToString("#.");
	}
	
}
