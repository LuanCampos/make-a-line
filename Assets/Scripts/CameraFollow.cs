using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	private Transform ball;
	private float ballPos;
	private float maxDistance = 2f;
	private bool lerpingFast = false;
	private int cameraShake;
	
    void Start()
    {
        ball = GameObject.Find("Ball").GetComponent<Transform>();
    }
	
	void LateUpdate()
    {
		ballPos = ball.position.x;
		CameraShake();
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
	
	public void CameraShake()
	{
		if (cameraShake > 0)
		{
			switch (cameraShake)
			{
				case 9:
					transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 3), .5f);
					break;
				case 7:
					transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -3), .5f);
					break;
				case 5:
					transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 3), .5f);
					break;
				case 3:
					transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -3), .5f);
					break;
				case 1:
					transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), .5f);
					break;
			}
			cameraShake --;
		}
	}
	
	public void ShakeCamera()
	{
		cameraShake = 9;
	}
	
}
