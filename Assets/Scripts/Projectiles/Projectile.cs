using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action<Enemy, float> OnEnemyHit;

    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] private float minDistanceToDealDamage = 0.1f;

    public TowerProjectile TowerOwner { get; set; }

    protected Enemy enemyTarget;

    protected virtual void Update()
    {
        if (enemyTarget != null)
        {
            MoveProjectile();
            RotateProjectile();
        }
    }

    protected virtual void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, enemyTarget.transform.position, moveSpeed * Time.deltaTime);
        float distanceToTarget = (enemyTarget.transform.position - transform.position).magnitude;
        if (distanceToTarget <= minDistanceToDealDamage)
        {
            OnEnemyHit?.Invoke(enemyTarget, damage);
            enemyTarget.EnemyHealth.DealDamage(damage);
            TowerOwner.ResetTowerProjectile();
            ObjectPooler.ReturnToPool(gameObject);
        }

    }

    private void RotateProjectile()
    {
        Vector3 enemyPos = enemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void SetEnemy(Enemy enemy)
    {
        enemyTarget = enemy;
    }

    public void ResetProjectile()
    {
        enemyTarget = null;
        transform.localRotation = Quaternion.identity;
    }
}
