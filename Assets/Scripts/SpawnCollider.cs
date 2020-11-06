using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
	[SerializeField]
	private GameObject spawnColliderPreset;
	
    private SpawnManager spawnManager;
	private float scale;
	
    void Start()
    {
        spawnManager = GameObject.Find("Player").GetComponent<SpawnManager>();
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.tag == "Ball")
		{
			if (transform.position.x > 0f)
			{
				scale = 1f;				
			}
			
			else
			{
				scale = -1f;
			}
			
			SpawnShape(20f * scale);
			SpawnNextCollider();
			Destroy(gameObject);
		}
		
	}
	
	private void SpawnShape(float offset)
	{
		spawnManager.SpawnShape(new Vector3(transform.position.x + offset, 0, 0));
	}
	
	private void InstantiateCollider(float offset)
	{
		Instantiate(spawnColliderPreset, new Vector3(transform.position.x + offset, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
	}
	
	private void SpawnNextCollider()
	{
		if (Mathf.Abs(transform.position.x) > 85)
		{
			InstantiateCollider(7f * scale);
		}
		
		else if (Mathf.Abs(transform.position.x) > 61)
		{
			InstantiateCollider(8f * scale);
		}
		
		else if (Mathf.Abs(transform.position.x) > 34)
		{
			InstantiateCollider(9f * scale);
		}
		
		else
		{
			InstantiateCollider(10f * scale);
		}
		
	}
	
}
