using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTowerProjectile : TowerProjectile
{
    protected override void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (tower.CurrentEnemyTarget != null && tower.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                FireProjectile(tower.CurrentEnemyTarget);
            }

            nextAttackTime = Time.time + delayBetweenAttacks;
        }
    }

    protected override void LoadProjectile()
    {

    }

    private void FireProjectile(Enemy enemy)
    {
        GameObject instance = objectPooler.GetPooledObject();
        instance.transform.position = projectileSpawnPosition.position;

        Projectile projectile = instance.GetComponent<Projectile>();
        currentProjectileLoaded = projectile;
        currentProjectileLoaded.TowerOwner = this;
        currentProjectileLoaded.ResetProjectile();
        currentProjectileLoaded.SetEnemy(enemy);
        currentProjectileLoaded.Damage = Damage;
        instance.SetActive(true);
    }
}
