using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPosition;
    [SerializeField] protected float delayBetweenAttacks = 2f;
    [SerializeField] protected float damage = 10f;

    public float Damage { get; set; }
    public float DelayPerShot { get; set; }

    protected float nextAttackTime;
    protected ObjectPooler objectPooler;
    protected Tower tower;
    protected Projectile currentProjectileLoaded;


    private void Start()
    {
        tower = GetComponent<Tower>();
        objectPooler = GetComponent<ObjectPooler>();

        DelayPerShot = delayBetweenAttacks;
        Damage = damage;
        LoadProjectile();
    }

    protected virtual void Update()
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

            nextAttackTime = Time.time + DelayPerShot;
        }

    }

    protected virtual void LoadProjectile()
    {
        GameObject newInstance = objectPooler.GetPooledObject();
        newInstance.transform.localPosition = projectileSpawnPosition.position;
        newInstance.transform.SetParent(projectileSpawnPosition);

        currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        currentProjectileLoaded.TowerOwner = this;
        currentProjectileLoaded.ResetProjectile();
        currentProjectileLoaded.Damage = Damage;
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
