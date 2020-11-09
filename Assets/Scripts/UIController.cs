using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
	private GameObject pausePanel;
	private GameObject gameOverPanel;
	private GameObject gameplayPanel;
	private GameManager gameManager;
	private TextMeshProUGUI finalScoreText;
	
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
	}
	
	public void PlayGame()
	{
		SceneManager.LoadScene("Gameplay");
	}
	
	public void Store()
	{
		GameObject.Find("Menu Panel").GetComponent<Animator>().Play("FadeOut");
		GameObject.Find("Store Panel").GetComponent<Animator>().Play("FadeIn");
	}
	
	public void BackToMenu()
	{
		GameObject.Find("Store Panel").GetComponent<Animator>().Play("FadeOut");
		GameObject.Find("Menu Panel").GetComponent<Animator>().Play("FadeIn");
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
		gameplayPanel.GetComponent<Animator>().Play("FadeOut");
		gameOverPanel.GetComponent<Animator>().Play("FadeIn");
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
		if (GameObject.Find("Pause Panel") != null)
		{
			pausePanel = GameObject.Find("Pause Panel");
			gameOverPanel = GameObject.Find("Game Over Panel");
			gameplayPanel = GameObject.Find("Gameplay Panel");
			finalScoreText = GameObject.Find("Final Score Text").GetComponent<TextMeshProUGUI>();
			pausePanel.SetActive(false);
			gameOverPanel.SetActive(false);
		}
	}
	
}
