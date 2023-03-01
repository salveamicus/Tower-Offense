using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("game");
    }

    public void TutorialGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ChangeTowers(int n)
    {
        gameStatistics.towersPerLevel = n;
    }
}
