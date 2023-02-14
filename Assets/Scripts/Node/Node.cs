using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;

    public Tower Tower { get; set; }

    public void SetTower(Tower tower)
    {
        Tower = tower;
    }

    public bool IsEmpty()
    {
        return Tower == null;
    }

    public void SelectTower()
    {
        OnNodeSelected?.Invoke(this);
    }
}
