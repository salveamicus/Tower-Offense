using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purchase_unit : MonoBehaviour
{
    public GameObject credits;
    public GameObject selected_prefab;
    
    public void purchase() {
        GameObject new_unit = Instantiate(selected_prefab, new Vector3(0,0,0), Quaternion.identity);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && selected_prefab) {
            Debug.Log("left click");
            Instantiate(selected_prefab, Input.mousePosition, Quaternion.identity);
        }
    }
}
