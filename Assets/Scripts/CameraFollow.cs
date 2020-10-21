using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	private Transform ball;
	private float ballPos;
	private float maxDistance = 2f;
	private bool lerpingFast = false;
	
    void Start()
    {
        ball = GameObject.Find("Ball").GetComponent<Transform>();
    }
	
	void LateUpdate()
    {
		ballPos = ball.position.x;
		LerpCamera();
    }

	private void LerpCamera()
	{
		if (lerpingFast)
		{
			CalculateLerp(maxDistance * 1/3);
		}
		
		else
		{
			CalculateLerp(maxDistance);
		}
	}
	
	private void CalculateLerp(float distance)
	{
		if (Mathf.Abs(transform.position.x - ballPos) <= distance)
		{
			ExecuteLerp(1.5f);
			lerpingFast = false;
		}
			
		else
		{
			ExecuteLerp(2f);
			lerpingFast = true;
		}
	}
	
	private void ExecuteLerp(float vel)
	{
		transform.position = Vector3.Lerp(transform.position, new Vector3(ballPos + 2.5f, 0f, -10f), vel * Time.deltaTime);
	}
	
}
