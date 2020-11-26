using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionManager : MonoBehaviour
{
	private CameraFollow cameraController;
	private GameManager gameManager;
	
	void Start()
    {
		cameraController = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		
		if (GameManager.instance != null)
		{
			gameManager = GameManager.instance;
		}
    }
	
	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Line")
        {
			gameManager.PlaySFX(0, 1f, Random.Range(.5f, 2f));
        }
		
		if (collision.gameObject.tag == "Shape")
        {
			gameManager.PlaySFX(4, .4f, Random.Range(.5f, 2f));
        }
    }
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "GetPoint")
        {
			gameManager.PlaySFX(1, 9f);
        }
		
		if (col.gameObject.tag == "GetTime")
        {
			gameManager.PlaySFX(2, .7f, 1.8f);
        }
		
		if (col.gameObject.tag == "LoseTime")
        {
			gameManager.PlaySFX(3, 9f);
			cameraController.ShakeCamera();
			//Handheld.Vibrate();
        }
	
	}
}
