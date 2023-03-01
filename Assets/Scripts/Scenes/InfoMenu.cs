using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour
{
    [SerializeField] GameObject infoMenu;

    public void PauseI()
    {
        infoMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        infoMenu.SetActive(false);
        Time.timeScale = 1f;
    }

}
