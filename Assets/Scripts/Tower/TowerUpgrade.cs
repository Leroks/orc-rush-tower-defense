using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    private TowerProjectile towerProjectile;

    // Start is called before the first frame update
    void Start()
    {
        towerProjectile = GetComponent<TowerProjectile>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeTower();
        }

    }

    private void UpgradeTower()
    {
        towerProjectile.Damage += damageIncremental;
        towerProjectile.DelayPerShot -= delayReduce;
    }
}
