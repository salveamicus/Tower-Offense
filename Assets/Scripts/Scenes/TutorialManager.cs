using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    
    public int currentState;
    int completedState;
    public GameObject[] panels;
    public GameObject storePanel;
    public GameObject tower;

    void Start()
    {
        currentState = 0;
        completedState = 2;
        foreach (GameObject panel in panels) {
            panel.SetActive(false);
        }
        panels[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (tower.GetComponent<GrandTower>().Health < 0) {
            currentState = 6;
            foreach (GameObject panel in panels) {
            panel.SetActive(false);
            }
            panels[6].SetActive(true);
        }
        if (currentState == 0) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                currentState = 1;
                panels[0].SetActive(false);
                panels[1].SetActive(true);
            }
        }
        else if (currentState == 1) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                currentState = 2;
                panels[1].SetActive(false);
                panels[2].SetActive(true);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                currentState = 0;
                panels[1].SetActive(false);
                panels[0].SetActive(true);
            }
        }
        else if (currentState == 2) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                currentState = 3;
                panels[2].SetActive(false);
                panels[3].SetActive(true);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                currentState = 1;
                panels[2].SetActive(false);
                panels[1].SetActive(true);
            }
        }
        else if (currentState == 3) {
            if (storePanel.GetComponent<storePanel>().selectedButton == 0 && completedState < 3) {
                completedState = 3;
                currentState = 4;
                panels[3].SetActive(false);
                panels[4].SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && completedState >= 3) {
                currentState = 4;
                panels[3].SetActive(false);
                panels[4].SetActive(true);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                currentState = 2;
                panels[3].SetActive(false);
                panels[2].SetActive(true);
            }
        }
        else if (currentState == 4) {
            if (GameObject.FindGameObjectsWithTag("Unit").Length > 0 && completedState < 4) {
                completedState = 4;
                currentState = 5;
                panels[4].SetActive(false);
                panels[5].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && completedState >= 4) {
                currentState = 5;
                panels[4].SetActive(false);
                panels[5].SetActive(true);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                currentState = 3;
                panels[4].SetActive(false);
                panels[3].SetActive(true);
            }
        }
        else if (currentState == 5) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                currentState = 4;
                panels[5].SetActive(false);
                panels[4].SetActive(true);
            }
        }
        else if (currentState == 6) {
            if (Input.anyKey) {
                gameStatistics.currentCredits = 2000;
                SceneManager.LoadScene("game");
            }
        }
    }
}