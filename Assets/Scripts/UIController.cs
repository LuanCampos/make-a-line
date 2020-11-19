﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
	private GameObject pausePanel;
	private GameObject gameOverPanel;
	private GameObject gameplayPanel;
	private GameObject smallLifePanel;
	private GameObject noLivesPanel;
	private GameObject noConnectionPanel;
	private GameObject weAreBackPanel;
	private GameObject bigLifePanel;
	private GameObject newHighScorePanel;
	private GameManager gameManager;
	private AdManager adManager;
	private TextMeshProUGUI finalScoreText;
	private TextMeshProUGUI highScoreText;
	
	void Awake()
	{
		GetGameplayPanels();
	}
	
	void Start()
	{
		if (GameManager.instance != null)
		{
			gameManager = GameManager.instance;
		}
		
		if (smallLifePanel.activeSelf)
		{
			ShowLives();
			ShowHighScore();
		}
	}
	
	public void PlayGame()
	{
		SceneManager.LoadScene("Gameplay");
	}
	
	public void Store()
	{
		SelectIcon();
		SelectLockers();
		GameObject.Find("Store High Score").GetComponent<TextMeshProUGUI>().text = gameManager.GetHighScore().ToString();
		GameObject.Find("Total Score").GetComponent<TextMeshProUGUI>().text = gameManager.GetTotalScore().ToString();
		GameObject.Find("Menu Panel").GetComponent<Animator>().Play("FadeOut");
		GameObject.Find("Store Panel").GetComponent<Animator>().Play("FadeIn");
	}
	
	public void BackToMenu()
	{
		GameObject.Find("Store Panel").GetComponent<Animator>().Play("FadeOut");
		GameObject.Find("Menu Panel").GetComponent<Animator>().Play("FadeIn");
		gameManager.SaveGame();
	}
	
	public void SelectLine()
	{
		int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
		
		if (GameManager.instance != null)
		{
			GameManager.instance.SelectLine(index);
			ShowSelectedIcon(index);
		}
	}
	
	public void Pause()
	{
		if (GameObject.Find("Initial Timer Text") == null)
		{
			pausePanel.SetActive(true);
			gameManager.SetIsTimeFreeze(true);
		}
	}
	
	public void Continue()
	{
		pausePanel.SetActive(false);
		gameManager.SetIsTimeFreeze(false);
	}
	
	public void ShowGameOverPanel()
	{
		finalScoreText.text = gameManager.GetLastScore().ToString("");
		gameOverPanel.SetActive(true);
		ShowLives();
		ShowHighScore();
		gameplayPanel.GetComponent<Animator>().Play("FadeOut");
		gameOverPanel.GetComponent<Animator>().Play("FadeIn");
		
		if (gameManager.lastScore > gameManager.GetHighScore())
		{
			gameManager.SetHighScore();
			newHighScorePanel.GetComponent<Animator>().Play("FadeIn");
		}
		
		gameManager.SaveGame();
	}
	
	public void ShowNoLivesPanel()
	{
		adManager.StartAd();
		noLivesPanel.SetActive(true);
		bigLifePanel.GetComponent<Animator>().Play("ZeroLives");
	}
	
	public void ShowNoConnectionPanel()
	{
		noLivesPanel.SetActive(false);
		bigLifePanel.GetComponent<Animator>().Play("Invisible");
		noConnectionPanel.SetActive(true);
	}
	
	public void ShowWeAreBackPanel()
	{
		gameManager.SaveGame();
		noLivesPanel.SetActive(false);
		noConnectionPanel.SetActive(false);
		weAreBackPanel.SetActive(true);
		StartCoroutine(ShowLivesAnimation());
	}
	
	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void ExitToMenu()
	{
		gameManager.SetIsTimeFreeze(false);
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}
	
	public void SharePrint()
	{
		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "MakeALineScore.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);

		new NativeShare().AddFile(filePath)
		//	.SetSubject("Make a Line - Score").SetText("See my new score, baby!")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
	}
	
	public void GetLivesButton()
	{
		adManager.ShowAd();
		GameObject.Find("Get Lives Button").GetComponent<Button>().interactable = false;
	}
	
	public void GetOneLifeButton()
	{
		gameManager.SetLives(1);
		ShowWeAreBackPanel();
	}
	
	private void ShowLives()
	{	
		switch (gameManager.GetLives())
        {
        case 3:
            smallLifePanel.GetComponent<Animator>().Play("Idle3");
            break;
        case 2:
            smallLifePanel.GetComponent<Animator>().Play("Idle2");
            break;
        case 1:
            smallLifePanel.GetComponent<Animator>().Play("Idle1");
            break;
        default:
            smallLifePanel.GetComponent<Animator>().Play("Idle0");
            break;
        }
	}
	
	private IEnumerator ShowLivesAnimation()
	{
		yield return new WaitForEndOfFrame();
		
		if (gameManager.GetLives() > 1)
		{
			bigLifePanel.GetComponent<Animator>().Play("ZeroToThree");
		}
		
		else
		{
			bigLifePanel.GetComponent<Animator>().Play("ZeroToOne");
		}
	}
	
	private void ShowHighScore()
	{
		highScoreText.text = gameManager.GetHighScore().ToString();
	}
	
	private void ShowSelectedIcon(int index)
	{
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("SelectLine");
		
		for (int i = 0; i < buttons.Length; i++)
		{
			if (int.Parse(buttons[i].name) == index)
			{
				buttons[i].transform.GetChild(0).gameObject.SetActive(true);
			}
			
			else
			{
				buttons[i].transform.GetChild(0).gameObject.SetActive(false);
			}
		}
	}
	
	private void GetGameplayPanels()
	{
		smallLifePanel = GameObject.Find("Small Life Panel");
		highScoreText = GameObject.Find("High Score").GetComponent<TextMeshProUGUI>();
		
		if (GameObject.Find("Pause Panel") != null)
		{
			pausePanel = GameObject.Find("Pause Panel");
			gameOverPanel = GameObject.Find("Game Over Panel");
			gameplayPanel = GameObject.Find("Gameplay Panel");
			noLivesPanel = GameObject.Find("No Lives Panel");
			noConnectionPanel = GameObject.Find("No Connection Panel");
			weAreBackPanel = GameObject.Find("We Are Back Panel");
			bigLifePanel = GameObject.Find("Big Life Panel");
			newHighScorePanel = GameObject.Find("New High Score Panel");
			adManager = GameObject.Find("Player").GetComponent<AdManager>();
			finalScoreText = GameObject.Find("Final Score Text").GetComponent<TextMeshProUGUI>();
			pausePanel.SetActive(false);
			gameOverPanel.SetActive(false);
			noLivesPanel.SetActive(false);
			noConnectionPanel.SetActive(false);
			weAreBackPanel.SetActive(false);
		}	
	}
	
	private void SelectIcon()
	{
		if (gameManager.linePrefab.name == "Line 3")
		{
			ShowSelectedIcon(3);
		}
		
		else if (gameManager.linePrefab.name == "Line 2")
		{
			ShowSelectedIcon(2);
		}
		
		else if (gameManager.linePrefab.name == "Line 1")
		{
			ShowSelectedIcon(1);
		}
		
		else
		{
			ShowSelectedIcon(0);
		}	
	}
	
	private void SelectLockers()
	{
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("SelectLine");
		
		for (int i = 0; i < buttons.Length; i++)
		{
			switch (i)
			{
				case 7:
					HideLockerOrNot(2, 1000000, buttons[i]);
					break;
				case 6:
					HideLockerOrNot(1, 7000, buttons[i]);
					break;
				case 5:
					HideLockerOrNot(2, 100000, buttons[i]);
					break;
				case 4:
					HideLockerOrNot(1, 5000, buttons[i]);
					break;
				case 3:
					HideLockerOrNot(2, 30000, buttons[i]);
					break;
				case 2:
					HideLockerOrNot(1, 3000, buttons[i]);
					break;
				case 1:
					HideLockerOrNot(1, 500, buttons[i]);
					break;
				default:
					break;
			}
		}
	}
	
	private void HideLockerOrNot(int option, int score, GameObject button)
	{
		if ((option == 1 && score <= gameManager.GetHighScore()) || (option == 2 && score <= gameManager.GetTotalScore()))
		{
			button.transform.GetChild(2).gameObject.SetActive(false);
		}
		
		else
		{
			button.GetComponent<Button>().interactable = false;
		}
	}
	
}
