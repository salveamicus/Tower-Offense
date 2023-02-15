using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("game");
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
