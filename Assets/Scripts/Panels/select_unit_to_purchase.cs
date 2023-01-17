using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_unit_to_purchase : MonoBehaviour
{
    public GameObject storePanel;
    public GameObject knight;
    public void selectKnight() {
        GameObject selected = storePanel.GetComponent<purchase_unit>().selected_prefab;
        if (GameObject.ReferenceEquals(selected, knight)) {
            storePanel.GetComponent<purchase_unit>().selected_prefab = null;
        }
        else {
            storePanel.GetComponent<purchase_unit>().selected_prefab = knight;
        }
    }
}
