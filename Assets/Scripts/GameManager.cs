using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public bool isTimeFreeze;
	public int lastScore;
	public GameObject linePrefab;
	
	[SerializeField]
	private GameObject[] lines;
	
	void Awake()
	{
		MakeSingleton();
	}
	
	void MakeSingleton()
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
	}
	
	public int GetLastScore()
	{
		return this.lastScore;
	}
	
}
