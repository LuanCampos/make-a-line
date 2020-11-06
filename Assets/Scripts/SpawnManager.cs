using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] shapes;
	
    void Start()
    {
        SpawnShape(new Vector3(10, 0, 0));
    }
	
	public void SpawnShape(Vector3 spawnPos)
	{
		Instantiate (shapes[Random.Range(0, shapes.Length)], spawnPos, Quaternion.Euler(new Vector3(0, 0, 0)));
	}
	
}
