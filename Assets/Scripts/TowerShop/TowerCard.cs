using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour
{
    public static Action<TowerSettings> OnTowerPlaced;

    [SerializeField] private Image towerImage;
    [SerializeField] private TextMeshProUGUI towerCost;

    public TowerSettings TowerLoaded { get; set; }

    public void SetupTowerButton(TowerSettings towerSettings)
    {
        TowerLoaded = towerSettings;
        towerImage.sprite = towerSettings.TowerShopSprite;
        towerCost.text = towerSettings.TowerShopCost.ToString();
    }

    public void PlaceTower()
    {
        if (CurrencySystem.Instance.TotalCoins >= TowerLoaded.TowerShopCost)
        {
            Debug.Log(CurrencySystem.Instance.TotalCoins);
            CurrencySystem.Instance.RemoveCoins(TowerLoaded.TowerShopCost);
            UIManager.Instance.CloseTowerShopPanel();
            OnTowerPlaced?.Invoke(TowerLoaded);
        }
    }
}
