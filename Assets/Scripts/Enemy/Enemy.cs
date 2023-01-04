using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Waypoint waypoint;

    public UnityEngine.Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(currentWaypointIndex);

    private int currentWaypointIndex;

    private void Start()
    {
        currentWaypointIndex = 0;
    }

    private void Update()
    {
        Move();
        if (CurrentPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime);
    }

    private bool CurrentPositionReached()
    {
        float distanceToNextPointPosition = UnityEngine.Vector3.Distance(transform.position, CurrentPointPosition);

        if (distanceToNextPointPosition <= 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = waypoint.Points.Length - 1;
        if (currentWaypointIndex < lastWaypointIndex)
        {
            currentWaypointIndex++;
        }
    }
}
