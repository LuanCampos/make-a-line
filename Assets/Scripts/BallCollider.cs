using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag != "Spawner")
		{
			Destroy(col.gameObject);
		}
	}
}
