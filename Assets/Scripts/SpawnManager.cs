using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] shapes;
	
	private GameObject shape;
	private Vector3 spawnPos = new Vector3(10, 0, 0);
	private int index;
	
    void Start()
    {
        SpawnShape();
    }
	
	private void SpawnShape()
	{
		index = Random.Range(0, shapes.Length);
        shape = shapes[index];
		GameObject newShape = Instantiate (shape, spawnPos, Quaternion.Euler(new Vector3(0, 0, 0)));
	}
	
}
