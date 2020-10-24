using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	private GameObject pausePanel;
	private GameObject gameOverPanel;
	
	void Awake()
	{
		GetGameplayPanels();
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
		pausePanel.SetActive(true);
	}
	
	public void Continue()
	{
		pausePanel.SetActive(false);
	}
	
	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void ExitToMenu()
	{
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
			pausePanel.SetActive(false);
			gameOverPanel.SetActive(false);
		}
	}
	
}
