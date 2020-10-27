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
			CheckAddScore();
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
			FinalTimer();
		}
    }
	
	private void CheckGameOver()
	{
		if (ball.position.y < -12 || timer <= 0.3f)
		{
			gameManager.SetLastScore((int)score);
			uiController.ShowGameOverPanel();
			gameManager.SetIsTimeFreeze(true);
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
		score = (60.4f - timer + ((maxballPos - minballPos) * 4)) * 2 + bonus;
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
	
	private void FinalTimer()
	{
		if (!finalTimerText.active)
		{
			finalTimerText.SetActive(true);
		}
		
		finalTimerText.GetComponent<TextMeshProUGUI>().text = timer.ToString("#.");
	}
	
}
