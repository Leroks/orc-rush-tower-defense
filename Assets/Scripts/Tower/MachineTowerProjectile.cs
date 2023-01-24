using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTowerProjectile : TowerProjectile
{
    [SerializeField] private bool isDualMachine;
    [SerializeField] private float spreadRange;

    protected override void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (tower.CurrentEnemyTarget != null)
            {
                Vector3 dirToTarget = tower.CurrentEnemyTarget.transform.position - transform.position;
                FireProjectile(dirToTarget);
            }

            nextAttackTime = Time.time + delayBetweenAttacks;
        }
    }

    protected override void LoadProjectile()
    {

    }

    private void FireProjectile(Vector3 direction)
    {
        GameObject instance = objectPooler.GetPooledObject();
        instance.transform.position = projectileSpawnPosition.position;

        MachineProjectile projectile = instance.GetComponent<MachineProjectile>();
        projectile.Direction = direction;

        if (isDualMachine)
        {
            float randomSpread = Random.Range(-spreadRange, spreadRange);
            Vector3 spread = new Vector3(0f, 0f, randomSpread);
            Quaternion spreadValue = Quaternion.Euler(spread);
            Vector2 newDirection = spreadValue * direction;
            projectile.Direction = newDirection;
        }

        instance.SetActive(true);
    }
}
