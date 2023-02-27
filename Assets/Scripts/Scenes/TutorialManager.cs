using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    
    public int currentState;
    int completedState;
    public GameObject[] panels;
    public GameObject storePanel;
    public GameObject tower;
    public GameObject selectKnight;
    public GameObject prevButton;
    public GameObject nextButton;
    public GameObject restartButton;

    void Start()
    {
        Button prev = prevButton.GetComponent<Button>();
        Button next = nextButton.GetComponent<Button>();
        Button restart = restartButton.GetComponent<Button>();
        prev.onClick.AddListener(onPrev);
        next.onClick.AddListener(onNext);
        restart.onClick.AddListener(onRestart);
        currentState = 0;
        completedState = 0;
        foreach (GameObject panel in panels) {
            panel.SetActive(false);
        }
        panels[0].SetActive(true);
        prevButton.SetActive(false);
        nextButton.SetActive(true);
        restartButton.SetActive(false);
    }

    void onPrev() {
        if (currentState > 0) {
            panels[currentState].SetActive(false);
            panels[currentState-1].SetActive(true);
            currentState -= 1;
            if (currentState == 0) {
                prevButton.SetActive(false);
            }
            if (currentState <= completedState) {
                nextButton.SetActive(true);
            }
        }
    }

    void onNext() {
        if (currentState < 2) {
            completedState = currentState + 1;
        }
        if (currentState == 2) {
            selectKnight.SetActive(true);
        }
        if (currentState == 9) {
            tower.GetComponent<GrandTower>().Health = 200;
            restartButton.SetActive(true);

        }
        if (currentState == 10) {
            SceneManager.LoadScene("game");
        }
        if (completedState >= currentState) {
            panels[currentState].SetActive(false);
            panels[currentState+1].SetActive(true);
            currentState += 1;
            if (completedState == currentState-1) {
                nextButton.SetActive(false);
            }
            if (currentState > 0) {
                prevButton.SetActive(true);
            }
        }
    }

    void onRestart() {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit")) {
            Destroy(unit);
        }
        gameStatistics.currentCredits = 60;
        tower.SetActive(true);
        tower.GetComponent<GrandTower>().Health = 100;
    }


    // Update is called once per frame
    void Update()
    {
        if (completedState == 2) {
            if (storePanel.GetComponent<storePanel>().selectedButton == 0) {
                panels[currentState].SetActive(false);
                completedState = 3;
                currentState = 4;
                panels[4].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(false);
            }
        }
        else if (completedState == 3) {
            if (GameObject.FindGameObjectsWithTag("Unit").Length > 0) {
                completedState = 4;
                panels[currentState].SetActive(false);
                currentState = 5;
                panels[5].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(false);
            }
        }
        else if (completedState == 4) {
            if (storePanel.GetComponent<storePanel>().selectedButton == -1) {
                completedState = 5;
                panels[currentState].SetActive(false);
                currentState = 6;
                panels[6].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(false);
            }
        }
        else if (completedState == 5) {
            if (Globals.SELECTED_UNITS.Count > 0 && Input.GetMouseButtonUp(0)) {
                completedState = 7;
                panels[currentState].SetActive(false);
                currentState = 7;
                panels[7].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(true);
            }
        }        
        else if (completedState == 7) {
            if (tower.GetComponent<GrandTower>().Health < 0) {
                completedState = 9;
                foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit")) {
                    Destroy(unit);
                }
                gameStatistics.currentCredits = 60;
                tower.GetComponent<GrandTower>().MaxHealth = 200;
                tower.GetComponent<GrandTower>().Health = 200;
                panels[currentState].SetActive(false);
                currentState = 9;
                panels[9].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(true);
            }
        }
        else if (completedState == 9) {
            if (tower.GetComponent<GrandTower>().Health < 0) {
                tower.SetActive(false);
                nextButton.SetActive(true);
                completedState = 10;
            }
        }
    }
}