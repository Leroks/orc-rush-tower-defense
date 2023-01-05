using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private float delayBetweenAttacks = 2f;

    private float nextAttackTime;
    private ObjectPooler objectPooler;
    private Tower tower;
    private Projectile currentProjectileLoaded;


    private void Start()
    {
        tower = GetComponent<Tower>();
        objectPooler = GetComponent<ObjectPooler>();

        LoadProjectile();
    }

    private void Update()
    {
        if (IsTowerEmpty())
        {
            LoadProjectile();
        }

        if (Time.time >= nextAttackTime)
        {
            if (tower.CurrentEnemyTarget != null && currentProjectileLoaded != null
            && tower.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                currentProjectileLoaded.transform.parent = null;
                currentProjectileLoaded.SetEnemy(tower.CurrentEnemyTarget);
            }

            nextAttackTime = Time.time + delayBetweenAttacks;
        }

    }

    private void LoadProjectile()
    {
        GameObject newInstance = objectPooler.GetPooledObject();
        newInstance.transform.localPosition = projectileSpawnPosition.position;
        newInstance.transform.SetParent(projectileSpawnPosition);

        currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        currentProjectileLoaded.TowerOwner = this;
        currentProjectileLoaded.ResetProjectile();
        newInstance.SetActive(true);
    }

    private bool IsTowerEmpty()
    {
        return currentProjectileLoaded == null;
    }

    public void ResetTowerProjectile()
    {
        currentProjectileLoaded = null;
    }
}
