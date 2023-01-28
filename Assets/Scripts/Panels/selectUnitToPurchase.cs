using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class selectUnitToPurchase : MonoBehaviour
{
    public void selectKnight() {
        int selected = transform.parent.GetComponent<storePanel>().selectedButton;
        if (selected == 0) {
            transform.parent.GetComponent<storePanel>().selectedButton = -1;
            transform.GetComponent<Image>().color = Color.white;
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower")) {
                tower.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else {
            transform.parent.GetComponent<storePanel>().selectedButton = 0;
            int index = transform.parent.GetComponent<storePanel>().selectedButton;
            if (index != -1) {
                transform.parent.GetChild(index).GetComponent<Image>().color = Color.white;
            }
            transform.GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f);
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower")) {
                tower.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        Debug.Log("number of units is " + Convert.ToString(gameStatistics.units.Count));
    }
}
