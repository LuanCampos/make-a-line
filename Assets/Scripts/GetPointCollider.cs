using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPointCollider : MonoBehaviour
{
	private GameplayManager gameplayManager;
	
	void Start()
    {
        gameplayManager = GameObject.Find("Player").GetComponent<GameplayManager>();
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.tag == "Ball")
		{
			gameplayManager.AddScore(150f);
			Destroy(gameObject);
		}
		
	}
}
