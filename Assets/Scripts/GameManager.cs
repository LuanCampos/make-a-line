﻿using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public bool isTimeFreeze;
	public int lastScore;
	public int highScore;
	public int totalScore;
	public int lives;
	public GameObject linePrefab;
	public GameObject[] lines;
	public AudioClip[] mySongs;
	private AudioSource musicSource;
	private AudioSource[] sfxSource = new AudioSource[3];
	private int usingSFX = 0;
	
	void Awake()
	{
		MakeSingleton();
		CreateAudioSources();
	}
	
	void Start()
	{
		MobileAds.Initialize(initStatus => { });
		LoadGame();
	}
	
	private void MakeSingleton()
	{
		if (instance != null)
		{
			Destroy (gameObject);
		}
		
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	
	private void CreateAudioSources()
	{
		musicSource = this.gameObject.AddComponent<AudioSource>();
		sfxSource[0] = this.gameObject.AddComponent<AudioSource>();
		sfxSource[1] = this.gameObject.AddComponent<AudioSource>();
		sfxSource[2] = this.gameObject.AddComponent<AudioSource>();
	}
	
	public void SelectLine(int index)
	{
		linePrefab = lines[index];
	}
	
	public GameObject GetSelectLine()
	{
		return this.linePrefab;
	}
	
	public void SetIsTimeFreeze(bool isFreeze)
	{
		this.isTimeFreeze = isFreeze;
	}
	
	public bool GetIsTimeFreeze()
	{
		return this.isTimeFreeze;
	}
	
	public void SetLastScore(int score)
	{
		this.lastScore = score;
		this.totalScore += score;
	}
	
	public int GetLastScore()
	{
		return this.lastScore;
	}
	
	public void SetHighScore()
	{
		this.highScore = this.lastScore;
	}
	
	public int GetHighScore()
	{
		return this.highScore;
	}
	
	public int GetTotalScore()
	{
		return this.totalScore;
	}
	
	public void SetLives(int life)
	{
		this.lives += life;
	}
	
	public int GetLives()
	{
		return this.lives;
	}
	
	public int GetIndexOfCurrentLine()
	{
		for (int i = 0; i < lines.Length; i++)
		{
			if(GameObject.ReferenceEquals(lines[i], linePrefab))
			{
				return i;
			}
		}
		return 0;
	}
	
	public void SetCurrentLineByIndex(int index)
	{
		linePrefab = lines[index];
	}
	
	public void LoadGame()
	{
		if(ES3.KeyExists("highScore"))
		{
			this.highScore = ES3.Load<int>("highScore");
		}
		
		if(ES3.KeyExists("totalScore"))
		{
			this.totalScore = ES3.Load<int>("totalScore");
		}
		
		if(ES3.KeyExists("lives"))
		{
			this.lives = ES3.Load<int>("lives");
		}
		
		if(ES3.KeyExists("currentLine"))
		{
			SetCurrentLineByIndex(ES3.Load<int>("currentLine"));
		}

	}
	
	public void SaveGame()
	{
		ES3.Save("highScore", this.highScore);
		ES3.Save("totalScore", this.totalScore);
		ES3.Save("lives", this.lives);
		ES3.Save("currentLine", GetIndexOfCurrentLine());
	}
	
	public void PlaySound(int index)
	{
		musicSource.loop = true;
		musicSource.clip = mySongs[index];
		musicSource.Play();
	}
	
	public void PlaySFX(int index, float volume)
	{
		PlaySFX(index, volume, 1f);
	}
	
	public void PlaySFX(int index, float volume, float pitch)
	{
		sfxSource[usingSFX].pitch = pitch;
		sfxSource[usingSFX].volume = volume;
		sfxSource[usingSFX].clip = mySongs[index];
		sfxSource[usingSFX].Play();
		
		if (usingSFX < 2)
		{
			usingSFX ++;
		}
		
		else
		{
			usingSFX = 0;
		}
	}
	
}
