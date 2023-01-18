using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class purchaseUnit : MonoBehaviour
{
    public GameObject gameStats;
    public GameObject selectedPrefab;
    public int cost;
    int currentCredits;
    Plane plane = new Plane(new Vector3(0,0,1), 0); // the xy plane
    int startMouseDown = 0;
    int framesUntilSpawn = 0;
    static int continuousSpawnStartDelay = gameStatistics.continuousSpawnStartDelay;
    static int continuousSpawnDelay = gameStatistics.continuousSpawnDelay;
    
    void Update() {
        if (framesUntilSpawn > 0) {
            framesUntilSpawn -= 1;
        }
        currentCredits = gameStats.GetComponent<gameStatistics>().currentCredits;
        if (Input.GetMouseButtonDown(0)) // left click 
        {
            startMouseDown = Time.frameCount;
            if (selectedPrefab && // there is a unit selected in the store
                currentCredits >= cost && // have enough credits
                Input.mousePosition.x < 400) // mouse position is not on the store panel
            {
                Vector3 screenPosition = Input.mousePosition;
                Vector3 scenePosition;
                float distance = 1;
                Ray ray = Camera.main.ScreenPointToRay(screenPosition);
                plane.Raycast(ray, out distance);
                scenePosition = ray.GetPoint(distance);
                Instantiate(selectedPrefab, scenePosition, Quaternion.identity);
                gameStats.GetComponent<gameStatistics>().currentCredits = currentCredits - cost;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if(Time.frameCount > startMouseDown + continuousSpawnStartDelay &&
                framesUntilSpawn == 0 &&
                selectedPrefab && // there is a unit selected in the store
                currentCredits >= cost && // have enough credits
                Input.mousePosition.x < 400) // mouse position is not on the store panel
            {
                Vector3 screenPosition = Input.mousePosition;
                Vector3 scenePosition;
                float distance = 1;
                Ray ray = Camera.main.ScreenPointToRay(screenPosition);
                plane.Raycast(ray, out distance);
                scenePosition = ray.GetPoint(distance);
                Instantiate(selectedPrefab, scenePosition, Quaternion.identity);
                gameStats.GetComponent<gameStatistics>().currentCredits = currentCredits - cost;
                framesUntilSpawn = continuousSpawnDelay;
            }
        }
        else if ( Input.GetMouseButtonUp(0))
        {

        }
        
    }
}
