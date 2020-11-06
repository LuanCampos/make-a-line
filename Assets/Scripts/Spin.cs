using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
	[SerializeField]
	private float rotationSpeed = .5f;
	[SerializeField]
	private float growSpeed = .005f;
	[SerializeField]
	private float minScale = .9f;
	[SerializeField]
	private float maxScale = 1.3f;
	
	private bool gettingBigger;
	
    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, rotationSpeed, Space.Self);
		
		if (transform.localScale.x <= minScale)
		{
			gettingBigger = true;
		}
		
		if (transform.localScale.x >= maxScale)
		{
			gettingBigger = false;
		}
		
		if (gettingBigger)
		{
			transform.localScale += new Vector3(growSpeed, growSpeed, 0);
		}
			
		else
		{
			transform.localScale -= new Vector3(growSpeed, growSpeed, 0);
		}
		
    }
	
}
