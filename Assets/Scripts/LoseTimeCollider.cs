using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTimeCollider : MonoBehaviour
{
	private GameplayManager gameplayManager;
	private CameraFollow cameraController;
	
	void Start()
    {
        gameplayManager = GameObject.Find("Player").GetComponent<GameplayManager>();
		cameraController = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.tag == "Ball")
		{
			gameplayManager.AddTime(-10f);
			cameraController.ShakeCamera();
			Destroy(gameObject);
		}
	}
}