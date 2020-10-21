using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
	[SerializeField]
	private float maxVelocity = 120f;
	
	private Rigidbody2D myBody;
	
	void Awake()
	{
		myBody = GetComponent<Rigidbody2D>();
	}
	
	private void FixedUpdate ()
	{
		if (myBody.velocity.sqrMagnitude > maxVelocity)
		{
			myBody.velocity *= 0.9f; // 0.5f is less smooth, 0.9999f is more smooth
		}
	}
	
}
