using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectUnitToPurchase : MonoBehaviour
{
    public GameObject storePanel;
    public GameObject knight;
    public int knightCost;
    public void selectKnight() {
        GameObject selected = storePanel.GetComponent<purchaseUnit>().selectedPrefab;
        if (GameObject.ReferenceEquals(selected, knight)) {
            storePanel.GetComponent<purchaseUnit>().selectedPrefab = null;
        }
        else {
            storePanel.GetComponent<purchaseUnit>().selectedPrefab = knight;
            storePanel.GetComponent<purchaseUnit>().cost = knightCost;
        }
    }
}
