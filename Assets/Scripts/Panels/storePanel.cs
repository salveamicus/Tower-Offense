using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class storePanel : MonoBehaviour
{
    // public GameObject gameStatistics;
    public int selectedButton = -1;
    public GameObject[] units;
    int[] costs = {gameStatistics.knightCost};
    int currentCredits;
    Plane plane = new Plane(new Vector3(0,0,1), 0); // the xy plane
    int startMouseDown = 0;
    int framesUntilSpawn = 0;
    int storePanelEdge = 800;
    static int continuousSpawnStartDelay = gameStatistics.continuousSpawnStartDelay;
    static int continuousSpawnDelay = gameStatistics.continuousSpawnDelay;
    
    void Update() {
        if (framesUntilSpawn > 0) {
            framesUntilSpawn -= 1;
        }
        currentCredits = gameStatistics.currentCredits;
        if (Input.GetMouseButtonDown(0)) // left click 
        {
            startMouseDown = Time.frameCount;
            if (selectedButton != -1 && // there is a unit selected in the store
                currentCredits >= costs[selectedButton] && // have enough credits
                Input.mousePosition.x < storePanelEdge) // mouse position is not on the store panel
            {
                Vector3 screenPosition = Input.mousePosition;
                Vector3 scenePosition;
                float distance = 1;
                Ray ray = Camera.main.ScreenPointToRay(screenPosition);
                plane.Raycast(ray, out distance);
                scenePosition = ray.GetPoint(distance);
                Instantiate(units[selectedButton], scenePosition, Quaternion.identity);
                gameStatistics.currentCredits = currentCredits - costs[selectedButton];
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if(Time.frameCount > startMouseDown + continuousSpawnStartDelay &&
                framesUntilSpawn == 0 &&
                selectedButton != -1 && // there is a unit selected in the store
                currentCredits >= costs[selectedButton] && // have enough credits
                Input.mousePosition.x < storePanelEdge) // mouse position is not on the store panel
            {
                Vector3 screenPosition = Input.mousePosition;
                Vector3 scenePosition;
                float distance = 1;
                Ray ray = Camera.main.ScreenPointToRay(screenPosition);
                plane.Raycast(ray, out distance);
                scenePosition = ray.GetPoint(distance);
                Instantiate(units[selectedButton], scenePosition, Quaternion.identity);
                gameStatistics.currentCredits = currentCredits - costs[selectedButton];
                framesUntilSpawn = continuousSpawnDelay;
            }
        }
        else if ( Input.GetMouseButtonUp(0))
        {

        }
        
    }
}
