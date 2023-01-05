using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;

    private ObjectPooler objectPooler;
    private Tower tower;
    private Projectile currentProjectileLoaded;


    private void Start()
    {
        tower = GetComponent<Tower>();
        objectPooler = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadProjectile();
        }

        if (tower.CurrentEnemyTarget != null && currentProjectileLoaded != null
        && tower.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
        {
            currentProjectileLoaded.transform.parent = null;
            currentProjectileLoaded.SetEnemy(tower.CurrentEnemyTarget);
        }
    }

    private void LoadProjectile()
    {
        GameObject newInstance = objectPooler.GetPooledObject();
        newInstance.transform.localPosition = projectileSpawnPosition.position;
        newInstance.transform.SetParent(projectileSpawnPosition);

        currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        newInstance.SetActive(true);
    }
}
