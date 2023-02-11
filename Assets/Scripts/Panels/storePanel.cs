using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class storePanel : MonoBehaviour
{
    // public GameObject gameStatistics;
    public int selectedButton = -1;
    public GameObject[] units;
    int currentCredits;
    Plane plane = new Plane(new Vector3(0,0,1), 0); // the xy plane
    int startMouseDown = 0;
    int framesUntilSpawn = 0;
    int storePanelEdge = 1920 - 195; // resolution width - store panel width
    static int continuousSpawnStartDelay = gameStatistics.continuousSpawnStartDelay;
    static int continuousSpawnDelay = gameStatistics.continuousSpawnDelay;
    public bool inTutorial;
    
    void PurchaseUnit(Vector3 screenPosition) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        float distance = 1f;
        plane.Raycast(ray, out distance);
        Vector3 scenePosition = ray.GetPoint(distance);

        if (gameStatistics.regeneratingLevel) return;
        if (inTutorial && (scenePosition.x > -3 || scenePosition.x < -7 || scenePosition.y > 3 || scenePosition.y < -3)) {
            return;
        }

        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower")) {
            if (Vector3.Distance(tower.transform.position, scenePosition) < tower.GetComponent<Tower>().ShootRadius) {
                return;
            }
        }
        Instantiate(units[selectedButton], scenePosition, Quaternion.identity);
        gameStatistics.currentCredits = currentCredits - gameStatistics.unitCosts[selectedButton];
    }

    void Start() {
        for (int i = 0; i < gameStatistics.unitCosts.Length; i++) {
            transform.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Convert.ToString(gameStatistics.unitCosts[i]);
        }
    }

    void Update() {
        if (framesUntilSpawn > 0) {
            framesUntilSpawn -= 1;
        }
        currentCredits = gameStatistics.currentCredits;
        if (Input.GetMouseButtonDown(0)) // left click 
        {
            startMouseDown = Time.frameCount;
            if (selectedButton != -1 && // there is a unit selected in the store
                currentCredits >= gameStatistics.unitCosts[selectedButton] && // have enough credits
                Input.mousePosition.x < storePanelEdge) // mouse position is not on the store panel
            {
                PurchaseUnit(Input.mousePosition);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if(Time.frameCount > startMouseDown + continuousSpawnStartDelay &&
                framesUntilSpawn == 0 &&
                selectedButton != -1 && // there is a unit selected in the store
                currentCredits >= gameStatistics.unitCosts[selectedButton] && // have enough credits
                Input.mousePosition.x < storePanelEdge) // mouse position is not on the store panel
            {
                PurchaseUnit(Input.mousePosition);
                framesUntilSpawn = continuousSpawnDelay;
            }
        }
        else if ( Input.GetMouseButtonUp(0))
        {

        }
        
    }
}
