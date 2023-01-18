using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePanel : MonoBehaviour
{
    public GameObject storePanel;
    public void changeStorePanel() {
        if (storePanel.activeInHierarchy) {
            storePanel.SetActive(false);
        }
        else {
            storePanel.SetActive(true);
        }
    }
}
