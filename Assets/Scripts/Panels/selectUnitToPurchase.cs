using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectUnitToPurchase : MonoBehaviour
{
    public GameObject knight;
    int knightCost = gameStatistics.knightCost;
    public void selectKnight() {
        GameObject selected = transform.parent.GetComponent<purchaseUnit>().selectedPrefab;
        if (GameObject.ReferenceEquals(selected, knight)) {
            transform.parent.GetComponent<purchaseUnit>().selectedPrefab = null;
        }
        else {
            transform.parent.GetComponent<purchaseUnit>().selectedPrefab = knight;
            transform.parent.GetComponent<purchaseUnit>().cost = knightCost;
        }
    }
}
