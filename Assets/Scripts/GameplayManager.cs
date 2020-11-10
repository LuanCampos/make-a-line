﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
	[HideInInspector]
	public float addScore;
	[HideInInspector]
	public float addTime;
	
	private Transform ball;
	private float timer;
	private float score;
	private float bonus;
	private TextMeshProUGUI scoreText;
	private TextMeshProUGUI timerText;
	private GameObject initialTimerText;
	private GameObject finalTimerText;
	private UIController uiController;
	private GameManager gameManager;
	
	void Awake()
	{
		ball = GameObject.Find("Ball").transform;
		scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
		timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();
		initialTimerText = GameObject.Find("Initial Timer Text");
		finalTimerText = GameObject.Find("Final Timer Text");
		timer = 60.4f;
		uiController = GameObject.Find("UI Controller").GetComponent<UIController>();
		initialTimerText.SetActive(false);
		finalTimerText.SetActive(false);
		
		if (GameManager.instance != null)
		{
			gameManager = GameManager.instance;
			gameManager.SetIsTimeFreeze(true);
			Time.timeScale = 0;
		}
	}
	
	void Start()
	{
		GameObject.Find("Gameplay Panel").GetComponent<Animator>().Play("FadeIn");
		StartCoroutine(InitialTimer());
	}
	
	void Update()
    {
		if (!gameManager.GetIsTimeFreeze())
		{
			if (Time.timeScale == 0)
			{
				Time.timeScale = 1;
			}
			
			CheckGameOver();
			CheckAddScoreAndTime();
			CountTime();
			CountScore();
		}
		
		else
		{
			if (Time.timeScale != 0)
			{
				Time.timeScale = 0;
			}
		}
		
		if (timer < 5.5f)
		{
			FinalTimerOn();
		}
		
		else
		{
			FinalTimerOff();
		}
    }
	
	public void AddScore(float addScore)
	{
		this.addScore += addScore;
	}
	
	public void AddTime(float addTime)
	{
		this.addTime += addTime;
	}
	
	private void CheckGameOver()
	{
		if (ball.position.y < -12 || timer <= 0.3f)
		{
			gameManager.SetLastScore(Mathf.RoundToInt(score));
			uiController.ShowGameOverPanel();
			gameManager.SetIsTimeFreeze(true);
		}
	}
	
	private void CheckAddScoreAndTime()
	{
		if (addScore > 0f)
		{
			bonus += addScore;
			addScore = 0f;
		}
		
		if (addTime != 0f)
		{
			if (timer + addTime > 0f)
			{
				timer += addTime;
			}
			
			else
			{
				timer = .4f;
			}
			
			addTime = 0f;
		}
	}
	
	private void CountTime()
	{
		timer -= Time.deltaTime;
		timerText.text = timer.ToString("#.");
	}
	
	private void CountScore()
	{
		score += Time.deltaTime * 10f + bonus;
		bonus = 0f;
		scoreText.text = score.ToString("#.");
	}
	
	IEnumerator InitialTimer()
	{
		initialTimerText.SetActive(true);
		
		for (int i = 3; i > 0; i--)
		{
			initialTimerText.GetComponent<TextMeshProUGUI>().text = i.ToString("");
			yield return new WaitForSecondsRealtime(1);
		}
		
		initialTimerText.SetActive(false);
		gameManager.SetIsTimeFreeze(false);
	}
	
	private void FinalTimerOn()
	{
		if (!finalTimerText.active)
		{
			finalTimerText.SetActive(true);
			GameObject.Find("Gameplay Panel").GetComponent<Animator>().Play("TimerFadeIn");
		}
		
		finalTimerText.GetComponent<TextMeshProUGUI>().text = timer.ToString("#.");
	}
	
	private void FinalTimerOff()
	{
		if (finalTimerText.active)
		{
			finalTimerText.SetActive(false);
		}
	}
	
}
