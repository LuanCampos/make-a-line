using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
	private LineRenderer lineRenderer;
	private EdgeCollider2D edgeCol;
	private List <Vector2> points;
	private float lineSize = 6f;
	
	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
		edgeCol = GetComponent<EdgeCollider2D>();
	}
	
	public void UpdateLine (Vector2 mousePos)
	{
		if (points == null)
		{
			SetInitialPoints(mousePos);
		}
		
		else
		{
			SetPoints(mousePos);
		}
	}
	
	private void SetInitialPoints (Vector2 mousePos)
	{
		points = new List<Vector2>();
		points.Add(mousePos);
		points.Add(mousePos + new Vector2(.001f, 0f));
		points.Add(mousePos + new Vector2(.002f, 0f));

		lineRenderer.positionCount = 3;
		UpdateLineRenderer();
		UpdateEdgeCol();
	}
	
	private void SetPoints (Vector2 mousePos)
	{
		Vector2 maxPoint = GetMaxPoint(mousePos);
		SetMaxPoint(maxPoint);
		SetMiddlePoint(maxPoint);
		UpdateLineRenderer();
		UpdateEdgeCol();
	}
	
	private void UpdateLineRenderer()
	{
		lineRenderer.SetPosition(0, points[0]);
		lineRenderer.SetPosition(1, points[1]);
		lineRenderer.SetPosition(2, points[2]);
	}
	
	private Vector2 GetMaxPoint (Vector2 mousePos)
	{
		if (Vector2.Distance(points[0], mousePos) > lineSize)
		{
			return points[0] + new Vector2(mousePos.x - points[0].x, mousePos.y - points[0].y).normalized * lineSize;
		}
		
		else
		{
			return mousePos;
		}
	}
	
	private void SetMaxPoint (Vector2 mousePos)
	{
		points[2] = mousePos;
	}
	
	private void SetMiddlePoint (Vector2 mousePos)
	{
		points[1] = new Vector2((points[0].x + points[2].x) / 2, (points[0].y + points[2].y) / 2);
	}
	
	private void UpdateEdgeCol()
	{
		edgeCol.points = points.ToArray();
	}
	
}
