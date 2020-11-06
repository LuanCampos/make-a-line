using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    private SpawnManager spawnManager;
	
    void Start()
    {
        spawnManager = GameObject.Find("Player").GetComponent<SpawnManager>();
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.tag == "Ball")
		{
			if (transform.position.x >= 0f)
			{
				spawnManager.SpawnShape(new Vector3(transform.position.x + 20f, 0, 0));
			}
			
			else
			{
				spawnManager.SpawnShape(new Vector3(transform.position.x - 20f, 0, 0));
			}
			
			Destroy(gameObject);
			
		}
		
	}
	
}
