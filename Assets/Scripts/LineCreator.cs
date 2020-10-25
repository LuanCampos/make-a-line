using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
	private GameObject linePrefab;
	private GameObject line;
	private Line activeLine;
	
	void Awake()
	{
		if (GameManager.instance != null)
		{
			linePrefab = GameManager.instance.linePrefab;
		}
	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			DestroyOldLine();
			MakeNewLine();
		}

		if (Input.GetMouseButtonUp(0))
		{
			activeLine = null;
		}

		if (activeLine != null)
		{
			UpdateLine();
		}

	}
	
	private void DestroyOldLine()
	{
		line = GameObject.FindWithTag("Line");
		
		if (line != null)
		{
			Destroy(line);
		}
	}
	
	private void MakeNewLine()
	{
		GameObject newLine = Instantiate(linePrefab);
		activeLine = newLine.GetComponent<Line>();
	}
	
	private void UpdateLine()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		activeLine.UpdateLine(mousePos);
	}

}
