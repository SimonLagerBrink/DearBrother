﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	public Collider2D platformCollider;
	public List<Transform> pathPoints;
	public bool isMovingOneDirection = false;
	public float moveSpeed = 5f;
	public float tolerance = 1f;

	private List<Collider2D> childrenColliders = new List<Collider2D>();

	private float overlapDistance = 0.075f;

	private int currentPositionInPath;
	private bool isMovingForward;
	private void OnDrawGizmos()
	{
		for (int i = 0; i < pathPoints.Count; i++)
		{
			Gizmos.DrawWireSphere(pathPoints[i].position, 0.05f);
			if (isMovingOneDirection)
			{
				if (i < pathPoints.Count - 1)
				{
					Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
				}
			}
			else
			{
				if (i < pathPoints.Count - 1)
				{
					Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
				}
				else
				{
					Gizmos.DrawLine(pathPoints[i].position, pathPoints[0].position);
				}
			}
		}
	}
	private void Start()
    {
		int closestIndex = 0;
		for (int i = 0; i < pathPoints.Count; i++)
		{
			if (Vector2.Distance(transform.position, pathPoints[i].position) < Vector2.Distance(transform.position, pathPoints[closestIndex].position))
			{
				closestIndex = i;
			}
		}
		currentPositionInPath = closestIndex;
    }

    private void FixedUpdate()
    {
		transform.position = Vector2.MoveTowards(transform.position, pathPoints[currentPositionInPath].position, Time.deltaTime * moveSpeed);
		if (Vector2.Distance(transform.position, pathPoints[currentPositionInPath].position) < tolerance)
		{
			if (!isMovingOneDirection)
			{
				currentPositionInPath++;
				if (currentPositionInPath == pathPoints.Count)
				{
					currentPositionInPath = 0;
				}
			}
			else
			{
				currentPositionInPath += isMovingForward ? 1 : -1;
				if (currentPositionInPath == pathPoints.Count)
				{
					isMovingForward = false;
					currentPositionInPath = pathPoints.Count - 1;
				}
				else if (currentPositionInPath == -1)
				{
					isMovingForward = true;
					currentPositionInPath = 1;
				}
			}
		}
    }
	private void Update()
	{
		Bounds bounds = platformCollider.bounds;
		float y = bounds.max.y;
		Vector2 position = new Vector2(bounds.center.x, y + overlapDistance * 0.5f);
		Vector2 size = new Vector2(bounds.size.x, overlapDistance);
		List<Collider2D> colliders = new List<Collider2D>();
		colliders.AddRange(Physics2D.OverlapBoxAll(position, size, 0));
		for (int i = 0; i < colliders.Count; i++)
		{
			if (!childrenColliders.Contains(colliders[i]))
			{
				if (colliders[i].gameObject.transform.parent.GetComponent<Rigidbody2D>() != null && colliders[i].gameObject.transform.parent.gameObject != gameObject)
				{
					colliders[i].gameObject.transform.parent.parent = gameObject.transform;
					childrenColliders.Add(colliders[i]);
				}
			}
		}
		for (int i = 0; i < childrenColliders.Count; i++)
		{
			if (!colliders.Contains(childrenColliders[i]))
			{
				childrenColliders[i].gameObject.transform.parent.parent = null;
				childrenColliders.Remove(childrenColliders[i]);
			}
		}
	}
}
