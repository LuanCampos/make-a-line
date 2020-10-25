using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public GameObject linePrefab;
	public bool isTimeFreeze;
	
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
	
	public bool GetIsTimeFreeze()
	{
		return this.isTimeFreeze;
	}
	
	public void SetIsTimeFreeze(bool isFreeze)
	{
		this.isTimeFreeze = isFreeze;
	}
	
}
