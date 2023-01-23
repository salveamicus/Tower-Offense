using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storeBtn : MonoBehaviour
{
    GameObject storePanel;
    public void changeStorePanel() {
        storePanel = transform.parent.GetChild(1).gameObject;
        if (storePanel.activeInHierarchy) {
            storePanel.SetActive(false);
        }
        else {
            storePanel.SetActive(true);
        }
    }
}
