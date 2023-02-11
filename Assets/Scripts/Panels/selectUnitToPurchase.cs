using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class selectUnitToPurchase : MonoBehaviour
{
    public void selectunit(int index) {
        int selected = transform.parent.GetComponent<storePanel>().selectedButton;
        if (selected == index) {
            transform.parent.GetComponent<storePanel>().selectedButton = -1;
            transform.GetComponent<Image>().color = Color.white;
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower")) {
                //tower.transform.GetChild(0).gameObject.SetActive(false);
                tower.gameObject.GetComponent<Tower>().rangeDisplayOverride = false;
            }
            gameStatistics.purchasingUnit = false;
        }
        else {
            transform.parent.GetComponent<storePanel>().selectedButton = index;
            int prev = transform.parent.GetComponent<storePanel>().selectedButton;
            if (prev != -1) {
                transform.parent.GetChild(index).GetComponent<Image>().color = Color.white;
            }
            transform.GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f);
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower")) {
                //tower.transform.GetChild(0).gameObject.SetActive(true);
                tower.gameObject.GetComponent<Tower>().rangeDisplayOverride = true;
            }
            gameStatistics.purchasingUnit = true;
        }
    }
}
