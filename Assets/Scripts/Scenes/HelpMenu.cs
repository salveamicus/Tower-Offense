using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenu : MonoBehaviour
{

    [SerializeField] GameObject helpMenu;

    public void PauseH()
    {
        helpMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        helpMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
