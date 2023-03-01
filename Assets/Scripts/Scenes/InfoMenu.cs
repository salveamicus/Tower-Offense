using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour
{
    [SerializeField] GameObject infoMenu;
    public Image sniperinfo;
    public Image supportInfo;
    public Image accelInfo;
    public Image fireInfo;
    public Image poisonInfo;
    public Image attractInfo;
    public Image lightningInfo;

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

    public void GenerateLevelLocks()
    {
        if (gameStatistics.levelNumber < LevelGenerator.sniperTowerThreshold)
        {
            sniperinfo.enabled = true;
        } else
        {
            sniperinfo.enabled = false;
        }

    }
}
