using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager1 : MonoBehaviour
{
    
    public int currentState;
    int completedState;
    public GameObject[] panels;
    public GameObject leftCube;
    public GameObject rightCube;
    public GameObject storePanel;
    public GameObject prevButton;
    public GameObject nextButton;

    void Start()
    {
        Button prev = prevButton.GetComponent<Button>();
        Button next = nextButton.GetComponent<Button>();
        prev.onClick.AddListener(onPrev);
        next.onClick.AddListener(onNext);
        currentState = 0;
        completedState = -1;
        foreach (GameObject panel in panels) {
            panel.SetActive(false);
        }
        rightCube.SetActive(false);
        leftCube.SetActive(true);
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
        if (currentState == 5) {
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
    void Update() {
        if (completedState == -1) {
            if (GameObject.FindGameObjectsWithTag("Unit").Length > 0) {
                completedState = 0;
                panels[0].SetActive(false);
                panels[1].SetActive(true);
                leftCube.SetActive(false);
                prevButton.SetActive(true);
                nextButton.SetActive(false);
            }
        }
        else if (completedState == 0) {
            if (storePanel.GetComponent<storePanel>().selectedButton == -1) {
                completedState = 1;
                currentState = 1;
                panels[1].SetActive(false);
                panels[2].SetActive(true);
                rightCube.SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(false);
            }
        }
        else if (completedState == 1) {
            if (Globals.SELECTED_UNITS.Count > 0 && Input.GetMouseButtonUp(0)) {
                completedState = 3;
                currentState = 3;
                panels[2].SetActive(false);
                panels[3].SetActive(true);
                rightCube.SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(true);
            }
        }
        else if (completedState == 3) {
            bool inBox = true;
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject unit in units) {
                    if (unit.transform.position.x < 2 || unit.transform.position.x > 6 ||
                        unit.transform.position.y > 3 || unit.transform.position.y < -3) {
                            inBox = false;
                            break;
                    }
            }
            if (units.Length > 0 && inBox == true) {
                currentState = 5;
                completedState = 5;
                foreach (GameObject panel in panels) {
                    panel.SetActive(false);
                }
                panels[5].SetActive(true);
                prevButton.SetActive(true);
                nextButton.SetActive(true);
            }
        }
        
        
        
        
    }
}