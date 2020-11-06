using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
	private Rigidbody2D rb;
	private Vector2 moveSpeed;
	private float rotationSpeed;
	
	void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();
		moveSpeed = new Vector2(Random.Range(-.015f, .015f), Random.Range(-.015f, .015f));
		rotationSpeed = Random.Range(-.65f, .65f);
	}
	
    void FixedUpdate()
    {
        if (transform.position.y < -9f || transform.position.y > 9f)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y * -.95f, transform.position.z);
		}
		
		rb.rotation += rotationSpeed;
		rb.position += moveSpeed;
    }
	
}
