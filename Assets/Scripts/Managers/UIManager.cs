using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject towerShopPanel;
    private Node currentNodeSelected;

    public void CloseTowerShopPanel()
    {
        towerShopPanel.SetActive(false);
    }

    private void NodeSelected(Node nodeSelected)
    {
        currentNodeSelected = nodeSelected;
        if (currentNodeSelected.IsEmpty())
        {
            towerShopPanel.SetActive(true);
        }
        else
        {
            // Show tower upgrade
        }
    }

    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }
}
