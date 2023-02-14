using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Shop Settings")]
public class TowerSettings : ScriptableObject
{
    public GameObject TowerPrefab;
    public int TowerShopCost;
    public Sprite TowerShopSprite;


}
