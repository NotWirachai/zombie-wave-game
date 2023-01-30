using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;

    [SerializeField] private float moveSpeed = 3f;

    public float MoveSpeed { get; set; }
    public Waypoint waypoint { get; set; }

    public EnemyHealth EnemyHealth { get; set; }

    public Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;
    private Vector3 _LastPointPosition;

    private EnemyHealth _enemyHealth;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        EnemyHealth = GetComponent<EnemyHealth>();

        _currentWaypointIndex = 0;
        MoveSpeed = moveSpeed;
        _LastPointPosition = transform.position;
 
    }

    private void Update()
    {
        Move();
        Rotate();

        if (CurrentPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement() 
    {
        MoveSpeed = 0;
    }

    public void ResumeMovement() 
    {
        MoveSpeed = moveSpeed;
    }

    private void Rotate() 
    {
        if (CurrentPointPosition.x > _LastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else 
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPositionReached() 
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f) 
        {
            _LastPointPosition = transform.position;
            return true;
        }
        return false;
    }

    private void UpdateCurrentPointIndex() 
    {
        int lastWaypointIndex = waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else 
        {
            EndPointReached();
        }
    }

    private void EndPointReached() 
    {
        OnEndReached?.Invoke(this);
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPooler(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }
}
