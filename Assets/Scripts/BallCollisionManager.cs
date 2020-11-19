using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionManager : MonoBehaviour
{
	private CameraFollow cameraController;
	
	void Start()
    {
		cameraController = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }
	
	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Line")
        {
			
        }
    }
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "GetPoint")
        {
			
        }
		
		if (col.gameObject.tag == "GetTime")
        {
			
        }
		
		if (col.gameObject.tag == "LoseTime")
        {
			cameraController.ShakeCamera();
			//Handheld.Vibrate();
        }
	}
}
