using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turret : MonoBehaviour
{
    [SerializeField] private float attackRange = 3f;

    public Enemy CurrentEnemyTarget { get; set; }
    public TurretUpgrade TurretUpgrade  { get; set; }
    public float AttackRange => attackRange;

    private bool _gameStarted;
    private List<Enemy> _enemies;

    private void Start()
    {
        _gameStarted = true;
        _enemies = new List<Enemy>();

        TurretUpgrade = GetComponent<TurretUpgrade>();
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget() 
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }

         Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
         float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
         transform.Rotate(0f, 0f, angle);
        // float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        // Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
       // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    private void GetCurrentEnemyTarget() 
    {
        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }

        CurrentEnemyTarget = _enemies[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (_enemies.Contains(enemy)) 
            {
                _enemies.Remove(enemy);
            }
        }
    } 

    private void OnDrawGizmos()
    {
        if (!_gameStarted)
        {
            GetComponent<CircleCollider2D>().radius = attackRange;
        }
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
