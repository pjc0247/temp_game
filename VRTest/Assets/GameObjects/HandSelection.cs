using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSelection : MonoBehaviour {
    public static HandSelection instance;

    public TowerBase selected { get; private set; }
    
    void Awake()
    {
        instance = this;
    }

    public void SelectTower(TowerBase tower)
    {
        if (selected != null)
            selected.OnUnselected();

        selected = tower;
        tower.OnSelected();
    }
    public void UnselectTower(TowerBase tower)
    {
        tower.OnUnselected();
        selected = null;
    }
}
