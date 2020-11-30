using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTimeCollider : MonoBehaviour
{
	[SerializeField]
	private GameObject particlesPrefab = null;	
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
			Instantiate(particlesPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		
	}
}