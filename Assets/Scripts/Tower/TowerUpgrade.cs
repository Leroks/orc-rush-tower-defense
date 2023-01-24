using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    public int UpgradeCost { get; set; }

    private TowerProjectile towerProjectile;

    // Start is called before the first frame update
    void Start()
    {
        towerProjectile = GetComponent<TowerProjectile>();
        UpgradeCost = upgradeInitialCost;
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
        if (CurrencySystem.Instance.TotalCoins >= UpgradeCost)
        {
            towerProjectile.Damage += damageIncremental;
            towerProjectile.DelayPerShot -= delayReduce;
            UpdateUpgrade();
        }

    }

    private void UpdateUpgrade()
    {
        CurrencySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
    }
}
