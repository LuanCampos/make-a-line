using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] shapes;
	public GameObject getPoint;
	public GameObject getTime;
	public GameObject loseTime;
	
    void Start()
    {
		InitialSpawn();
    }
	
	public void SpawnShape(Vector3 spawnPos)
	{
		Instantiate (shapes[Random.Range(0, shapes.Length)], spawnPos, Quaternion.Euler(new Vector3(0, 0, 0)));
	}
	
	public void SpawnGetPoint(Vector3 spawnPos)
	{
		Instantiate (getPoint, spawnPos, Quaternion.Euler(new Vector3(0, 0, 0)));
	}
	
	public void SpawnGetTime(Vector3 spawnPos)
	{
		Instantiate (getTime, spawnPos, Quaternion.Euler(new Vector3(0, 0, 0)));
	}
	
	public void SpawnLoseTime(Vector3 spawnPos)
	{
		Instantiate (loseTime, spawnPos, Quaternion.Euler(new Vector3(0, 0, 0)));
	}
	
	private void InitialSpawn()
	{
		SpawnShape(new Vector3(10, 0, 0));	
		SpawnGetPoint(new Vector3(Random.Range(-7f, -2f), Random.Range(-2.5f, 2.5f), 0));
		SpawnGetPoint(new Vector3(Random.Range(3f, 12f), Random.Range(-2.5f, 2.5f), 0));
	}
	
}
