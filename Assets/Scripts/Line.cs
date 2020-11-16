using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
	private LineRenderer lineRenderer;
	private EdgeCollider2D edgeCol;
	private List <Vector2> points;
	private float lineSize = 6f;
	private Transform ball;
	
	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
		edgeCol = GetComponent<EdgeCollider2D>();
		ball = GameObject.Find("Ball").transform;
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
		Vector2 ballIsBetweenPoints = BallIsBetweenPoints(maxPoint);
		
		while (maxPoint != ballIsBetweenPoints)
		{
			maxPoint = ballIsBetweenPoints;
			ballIsBetweenPoints = BallIsBetweenPoints(maxPoint);
		}
		
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
	
	private Vector2 BallIsBetweenPoints(Vector2 mousePos)
	{
		float ballX = ball.position.x - points[0].x;
		float ballY = ball.position.y - points[0].y;
		float oldLineX = points[2].x - points[0].x;
		float oldLineY = points[2].y - points[0].y;
		float newLineX = mousePos.x - points[0].x;
		float newLineY = mousePos.y - points[0].y;
		
		if (newLineX != oldLineX || newLineY != oldLineY)
		{
			float ballLineLength = Mathf.Sqrt(ballX * ballX + ballY * ballY);
			float oldLineLength = Mathf.Sqrt(oldLineX * oldLineX + oldLineY * oldLineY);
			float newLineLength = Mathf.Sqrt(newLineX * newLineX + newLineY * newLineY);
			
			if (ballLineLength < oldLineLength)
			{
				float oldLineYInBallX = oldLineY / oldLineX * ballX;
				float newLineYInBallX = newLineY / newLineX * ballX;
				
				if ((oldLineYInBallX < ballY - .2f && newLineYInBallX > ballY - .2f) || (oldLineYInBallX > ballY + .2f && newLineYInBallX < ballY + .2f))
				{
					return new Vector2((points[2].x + mousePos.x) / 2, (points[2].y + mousePos.y) / 2);
				}
			}

		}
		return mousePos;
	}
	
}
