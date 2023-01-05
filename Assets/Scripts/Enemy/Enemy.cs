using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEnemyReachedEnd;

    [SerializeField] private float moveSpeed = 3f;
    public float MoveSpeed { get; set; }

    public Waypoint Waypoint { get; set; }

    public UnityEngine.Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(currentWaypointIndex);

    private int currentWaypointIndex;
    private UnityEngine.Vector3 lastPointPosition;

    private EnemyHealth enemyHealth;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentWaypointIndex = 0;
        MoveSpeed = moveSpeed;
        lastPointPosition = transform.position;

    }

    private void Update()
    {
        Move();
        Rotate();

        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumeMovement()
    {
        MoveSpeed = moveSpeed;
    }

    private void Rotate()
    {
        if (CurrentPointPosition.x > lastPointPosition.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = UnityEngine.Vector3.Distance(transform.position, CurrentPointPosition);

        if (distanceToNextPointPosition <= 0.1f)
        {
            lastPointPosition = transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (currentWaypointIndex < lastWaypointIndex)
        {
            currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEnemyReachedEnd?.Invoke(this);
        enemyHealth.ResetHealth();
        ObjectPooler.ReturToPool(gameObject);
    }

    public void ResetEnemy()
    {
        currentWaypointIndex = 0;
    }
}
