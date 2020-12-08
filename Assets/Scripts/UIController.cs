using System.IO;
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
	private GameObject newLinePanel;
	private GameManager gameManager;
	private AdManager adManager;
	private TextMeshProUGUI finalScoreText;
	private TextMeshProUGUI highScoreText;
	private int lastHighScore;
	private int nextCheckIndex;
	
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
		
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			ShowLives();
			ShowHighScore();
			//gameManager.PlaySound(6);
		}
		
		else
		{
			//gameManager.PlaySound(7);
		}
	}
	
	public void PlayGame()
	{
		SceneManager.LoadScene("Gameplay");
	}
	
	public void Store()
	{
		SelectLockers();
		SelectIcon();
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
		lastHighScore = gameManager.GetHighScore();
		
		if (gameManager.lastScore > lastHighScore)
		{
			gameManager.SetHighScore();
			newHighScorePanel.GetComponent<Animator>().Play("FadeIn");
		}
		
		gameManager.SaveGame();
		CheckIfUnlockNewLine(1);
	}
	
	public void ShowNoLivesPanel()
	{
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
	
	public void BackToGameOverPanel()
	{
		newLinePanel.SetActive(false);
		gameOverPanel.SetActive(true);
		ShowLives();
		gameOverPanel.GetComponent<Animator>().Play("FadeIn");
		
		if (gameManager.lastScore == gameManager.GetHighScore())
		{
			newHighScorePanel.GetComponent<Animator>().Play("FadeIn");
		}
		
		CheckIfUnlockNewLine(nextCheckIndex);
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
	
	public void PlayButtonSound()
	{
		gameManager.PlaySFX(5, .5f);
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
			newLinePanel = GameObject.Find("New Line Panel");
			adManager = GameObject.Find("Game Manager").GetComponent<AdManager>();
			finalScoreText = GameObject.Find("Final Score Text").GetComponent<TextMeshProUGUI>();
			pausePanel.SetActive(false);
			gameOverPanel.SetActive(false);
			newLinePanel.SetActive(false);
			noLivesPanel.SetActive(false);
			noConnectionPanel.SetActive(false);
			weAreBackPanel.SetActive(false);
		}	
	}
	
	private void SelectIcon()
	{
		for (int i = 0; i < gameManager.lines.Length; i++)
		{
			if (gameManager.linePrefab == gameManager.lines[i])
			{
				ShowSelectedIcon(i);
			}
		}
	}
	
	private void SelectLockers()
	{
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("SelectLine");
		
		for (int i = 0; i < buttons.Length; i++)
		{
			switch (int.Parse(buttons[i].name))
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
					HideLockerOrNot(1, 1000, buttons[i]);
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
	
	private void CheckIfUnlockNewLine(int index)
	{
		switch(index)
        {
        case 1:
            if (gameManager.GetHighScore() >= 1000 && lastHighScore < 1000)
			{
				ShowNewLinePanel(1);
				nextCheckIndex = 2;
			}
			else
			{
				CheckIfUnlockNewLine(2);
			}
            break;
        case 2:
            if (gameManager.GetHighScore() >= 3000 && lastHighScore < 3000)
			{
				ShowNewLinePanel(2);
				nextCheckIndex = 3;
			}
			else
			{
				CheckIfUnlockNewLine(3);
			}
            break;
        case 3:
            if (gameManager.GetTotalScore() >= 30000 && gameManager.GetTotalScore() - gameManager.GetLastScore() < 30000)
			{
				ShowNewLinePanel(3);
				nextCheckIndex = 4;
			}
			else
			{
				CheckIfUnlockNewLine(4);
			}
            break;
		case 4:
            if (gameManager.GetHighScore() >= 5000 && lastHighScore < 5000)
			{
				ShowNewLinePanel(4);
				nextCheckIndex = 5;
			}
			else
			{
				CheckIfUnlockNewLine(5);
			}
            break;
		case 5:
            if (gameManager.GetTotalScore() >= 100000 && gameManager.GetTotalScore() - gameManager.GetLastScore() < 100000)
			{
				ShowNewLinePanel(5);
				nextCheckIndex = 6;
			}
			else
			{
				CheckIfUnlockNewLine(6);
			}
            break;
		case 6:
            if (gameManager.GetHighScore() >= 7000 && lastHighScore < 7000)
			{
				ShowNewLinePanel(6);
				nextCheckIndex = 7;
			}
			else
			{
				CheckIfUnlockNewLine(7);
			}
            break;
		case 7:
            if (gameManager.GetTotalScore() >= 1000000 && gameManager.GetTotalScore() - gameManager.GetLastScore() < 1000000)
			{
				ShowNewLinePanel(7);
				nextCheckIndex = 0;
			}
            break;
        default:
            break;
        }
		
	}
	
	private void ShowNewLinePanel(int index)
	{
		newLinePanel.SetActive(true);
		
		GameObject[] lines = GameObject.FindGameObjectsWithTag("SelectLine");
		
		for (int i = 0; i < lines.Length; i++)
		{
			if (int.Parse(lines[i].name) == index)
			{
				lines[i].SetActive(true);
			}
			
			else
			{
				lines[i].SetActive(false);
			}
		}
		
		gameOverPanel.SetActive(false);
		newLinePanel.GetComponent<Animator>().Play("FadeIn");
	}
	
}
