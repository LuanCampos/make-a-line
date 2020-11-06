using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTimeCollider : MonoBehaviour
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
			gameplayManager.AddTime(10f);
			Destroy(gameObject);
		}
		
	}
}