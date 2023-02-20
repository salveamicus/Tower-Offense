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
    public GameObject prevButton;
    public GameObject nextButton;

    void Start()
    {
        Button prev = prevButton.GetComponent<Button>();
        Button next = nextButton.GetComponent<Button>();
        prev.onClick.AddListener(onPrev);
        next.onClick.AddListener(onNext);
        currentState = 0;
        completedState = 2;
        foreach (GameObject panel in panels) {
            panel.SetActive(false);
        }
        panels[0].SetActive(true);
        prevButton.SetActive(false);
        nextButton.SetActive(true);
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
        if (currentState == 9) {
            SceneManager.LoadScene("game");
        }
        else if (completedState >= currentState) {
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

    // Update is called once per frame
    void Update()
    {
        if (completedState == 2) {
            if (storePanel.GetComponent<storePanel>().selectedButton == 0) {
                completedState = 3;
                currentState = 4;
                panels[3].SetActive(false);
                panels[4].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(false);
            }
        }
        else if (completedState == 3) {
            if (GameObject.FindGameObjectsWithTag("Unit").Length > 0) {
                completedState = 4;
                currentState = 5;
                panels[4].SetActive(false);
                panels[5].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(false);
            }
        }
        else if (completedState == 4) {
            if (storePanel.GetComponent<storePanel>().selectedButton == -1) {
                completedState = 5;
                currentState = 6;
                panels[5].SetActive(false);
                panels[6].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(false);
            }
        }
        else if (completedState == 5) {
            if (Globals.SELECTED_UNITS.Count > 0 && Input.GetMouseButtonUp(0)) {
                completedState = 7;
                currentState = 7;
                panels[6].SetActive(false);
                panels[7].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(true);
            }
        }        
        else if (completedState == 7) {
            if (tower.GetComponent<GrandTower>().Health < 0) {
                currentState = 9;
                completedState = 9;
                foreach (GameObject panel in panels) {
                panel.SetActive(false);
                }
                panels[9].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(true);
            }
        }
    }
}