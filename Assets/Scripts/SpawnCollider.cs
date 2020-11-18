using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
	public GameObject spawnColliderPreset;
	
    private SpawnManager spawnManager;
	private float scale;
	private float positionX;
	
    void Start()
    {
        spawnManager = GameObject.Find("Player").GetComponent<SpawnManager>();
		positionX = transform.position.x;
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.tag == "Ball")
		{
			if (positionX > 0f)
			{
				scale = 1f;				
			}
			
			else
			{
				scale = -1f;
			}
			
			SpawnShape(20f * scale);
			NextColliderAndCollectables();
			Destroy(gameObject);
		}
		
	}
	
	private void SpawnShape(float offset)
	{
		spawnManager.SpawnShape(new Vector3(positionX + offset, 0, 0));
	}
	
	private void InstantiateCollider(float offset)
	{
		Instantiate(spawnColliderPreset, new Vector3(positionX + offset, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
	}
	
	private void NextColliderAndCollectables()
	{
		if (Mathf.Abs(positionX) > 85)
		{
			InstantiateCollider(7f * scale);
			
			if (Random.Range(1,3) == 1)
			{
				SpawnCollectables(3, new Vector3(positionX + Random.Range(10f, 20f) * scale, Random.Range(-2.7f, 2.7f), 0));
			}
			
			else
			{
				SpawnCollectables(Random.Range(1,3), new Vector3(positionX + Random.Range(10f, 20f) * scale, Random.Range(-2.7f, 2.7f), 0));
			}
		}
		
		else if (Mathf.Abs(positionX) > 61)
		{
			InstantiateCollider(8f * scale);
			SpawnCollectables(Random.Range(1,4), new Vector3(positionX + Random.Range(10f, 20f) * scale, Random.Range(-2.7f, 2.7f), 0));
		}
		
		else if (Mathf.Abs(positionX) > 34)
		{
			InstantiateCollider(9f * scale);
			SpawnCollectables(1, new Vector3(positionX + Random.Range(10f, 20f) * scale, Random.Range(-2.7f, 2.7f), 0));
			SpawnCollectables(Random.Range(1,4), new Vector3(positionX + Random.Range(10f, 20f) * scale, Random.Range(-2.7f, 2.7f), 0));
		}
		
		else
		{
			InstantiateCollider(10f * scale);
			SpawnCollectables(1, new Vector3(positionX + Random.Range(10f, 20f) * scale, Random.Range(-2.7f, 2.7f), 0));
			SpawnCollectables(Random.Range(1,3), new Vector3(positionX + Random.Range(10f, 20f) * scale, Random.Range(-2.7f, 2.7f), 0));
		}
	}
	
	private void SpawnCollectables(int index, Vector3 spawnPos)
	{
		switch (index)
        {
        case 3:
			spawnManager.SpawnLoseTime(spawnPos);
            break;
        case 2:
            spawnManager.SpawnGetTime(spawnPos);
            break;
        default:
            spawnManager.SpawnGetPoint(spawnPos);
            break;
		}
	}
	
}
