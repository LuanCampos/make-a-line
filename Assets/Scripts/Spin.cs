using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
	private Rigidbody2D rb;
	//private Vector2 moveSpeed;
	//private float rotationSpeed;
	
	void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();
		//moveSpeed = ;
		//rotationSpeed = ;
	}
	
    void FixedUpdate()
    {
        if (transform.position.y < -9f || transform.position.y > 9f)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y * -.95f, transform.position.z);
		}
		
		rb.rotation += .5f;
		rb.position += new Vector2(.01f, .01f);
    }
	
}
