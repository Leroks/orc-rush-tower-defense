using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShopManager : MonoBehaviour
{
    [SerializeField] private GameObject towerCardPrefab;
    [SerializeField] private Transform towerPanelContainer;

    [Header("Tower Settings")]
    [SerializeField] private TowerSettings[] towers;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < towers.Length; i++)
        {
            CreateTowerCard(towers[i]);
        }
    }

    private void CreateTowerCard(TowerSettings towerSettings)
    {
        GameObject newInstance = Instantiate(towerCardPrefab, towerPanelContainer.position, Quaternion.identity);
        newInstance.transform.SetParent(towerPanelContainer);
        newInstance.transform.localScale = Vector3.one;

        TowerCard cardButton = newInstance.GetComponent<TowerCard>();
        cardButton.SetupTowerButton(towerSettings);
    }
}
