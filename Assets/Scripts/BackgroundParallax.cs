using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
	public float offset; // -20f, 0f, 20f
	private Transform cam;
    
    void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
    }
	
	void FixedUpdate()
	{
		if (Mathf.Abs(cam.position.x - transform.position.x) > 35f)
		{
			if (cam.position.x - transform.position.x > 0f)
			{
				offset += 60f;
			}
			
			else
			{
				offset -= 60f;
			}
		}
	}
   
    void LateUpdate()
    {		
		transform.position = Vector3.Lerp (transform.position, new Vector3(cam.position.x / 1.05f + offset, transform.position.y, transform.position.z), 5f);
    }
	
}
