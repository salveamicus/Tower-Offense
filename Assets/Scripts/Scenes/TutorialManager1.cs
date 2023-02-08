using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager1 : MonoBehaviour
{
    
    public int currentState;
    int completedState;
    public GameObject[] panels;

    void Start()
    {
        currentState = 0;
        completedState = 0;
        foreach (GameObject panel in panels) {
            panel.SetActive(false);
        }
        panels[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {   bool inBox = true;
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units) {
                if (unit.transform.position.x < 2 || unit.transform.position.x > 6 ||
                    unit.transform.position.y > 3 || unit.transform.position.y < -3) {
                        inBox = false;
                        break;
                    }
            }
        if (units.Length > 0 && inBox == true) {
            currentState = 4;
            foreach (GameObject panel in panels) {
                panel.SetActive(false);
            }
            panels[4].SetActive(true);
        }
        else if (currentState == 0) {
            if (GameObject.FindGameObjectsWithTag("Unit").Length > 0 && Input.GetKeyDown(KeyCode.RightArrow)) {
                currentState = 1;
                panels[0].SetActive(false);
                panels[1].SetActive(true);
            }
        }
        else if (currentState == 1) {
            // if left mouse button up, go to next panel
            if (Input.GetMouseButtonUp(0)) {
                completedState = 1;
                currentState = 2;
                panels[1].SetActive(false);
                panels[2].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && completedState >= 1) {
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
            if (Input.GetKeyDown(KeyCode.RightArrow) && completedState == 2) {
                currentState = 3;
                panels[2].SetActive(false);
                panels[3].SetActive(true);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow) && completedState == 2) {
                currentState = 1;
                panels[2].SetActive(false);
                panels[1].SetActive(true);
            } else if (Input.anyKey && completedState < 2) {
                completedState = 2;
                currentState = 3;
                panels[2].SetActive(false);
                panels[3].SetActive(true);
            } 
        }
        else if (currentState == 3) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                currentState = 2;
                panels[3].SetActive(false);
                panels[2].SetActive(true);
            }
        }
        else if (currentState == 4) {
            if (Input.anyKey) {
                SceneManager.LoadScene("game");
            }
        }
    }
}