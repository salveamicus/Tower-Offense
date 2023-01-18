using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class purchaseUnit : MonoBehaviour
{
    public GameObject amount;
    public GameObject selectedPrefab;
    public int cost;
    Plane plane = new Plane(new Vector3(0,0,1), 0);
    
    void LateUpdate() {
        if (Input.GetMouseButtonDown(0) && selectedPrefab && Convert.ToInt32(amount.GetComponent<Text>().text) >= cost) {
            Vector3 screenPosition = Input.mousePosition;
            Vector3 scenePosition;
            float distance = 1;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            plane.Raycast(ray, out distance);
            scenePosition = ray.GetPoint(distance);
            Instantiate(selectedPrefab, scenePosition, Quaternion.identity);
            amount.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(amount.GetComponent<Text>().text) - cost);
        }
    }
}
