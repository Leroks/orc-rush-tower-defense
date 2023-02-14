using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShopManager : MonoBehaviour
{
    [SerializeField] private GameObject towerCardPrefab;
    [SerializeField] private Transform towerPanelContainer;

    [Header("Tower Settings")]
    [SerializeField] private TowerSettings[] towers;

    private Node currentNodeSelected;

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


    private void NodeSelected(Node nodeSelected)
    {
        currentNodeSelected = nodeSelected;
    }

    private void PlaceTower(TowerSettings towerLoaded)
    {
        if (currentNodeSelected != null)
        {
            GameObject towerInstance = Instantiate(towerLoaded.TowerPrefab);
            towerInstance.transform.localPosition = currentNodeSelected.transform.position;
            towerInstance.transform.parent = currentNodeSelected.transform;

            Tower towerPlaced = towerInstance.GetComponent<Tower>();
            currentNodeSelected.SetTower(towerPlaced);
        }
    }


    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
        TowerCard.OnTowerPlaced += PlaceTower;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
        TowerCard.OnTowerPlaced -= PlaceTower;
    }
}
