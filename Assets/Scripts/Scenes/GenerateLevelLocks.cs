using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLevelLocks : MonoBehaviour
{
    public GameObject sniperlock;
    public GameObject supportlock;
    public GameObject accellock;
    public GameObject firelock;
    public GameObject poisonlock;
    public GameObject temporallock;
    public GameObject attractlock;
    public GameObject lightninglock;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatistics.levelNumber >= LevelGenerator.sniperTowerThreshold)
        {
            sniperlock.SetActive(false);
        }
        else
        {
            sniperlock.SetActive(true);
        }

        if (gameStatistics.levelNumber >= LevelGenerator.supportTowerThreshold)
        {
            supportlock.SetActive(false);
        }
        else
        {
            supportlock.SetActive(true);
        }

        if (gameStatistics.levelNumber >= LevelGenerator.accelerationTowerThreshold)
        {
            accellock.SetActive(false);
        }
        else
        {
            accellock.SetActive(true);
        }

        if (gameStatistics.levelNumber >= LevelGenerator.fireTowerThreshold)
        {
            firelock.SetActive(false);
        }
        else
        {
            firelock.SetActive(true);
        }

        if (gameStatistics.levelNumber >= LevelGenerator.poisonTowerThreshold)
        {
            poisonlock.SetActive(false);
        }
        else
        {
            poisonlock.SetActive(true);
        }

        if (gameStatistics.levelNumber >= LevelGenerator.temporalTowerThreshold)
        {
            temporallock.SetActive(false);
        }
        else
        {
            temporallock.SetActive(true);
        }

        if (gameStatistics.levelNumber >= LevelGenerator.attractorTowerThreshold)
        {
            attractlock.SetActive(false);
        }
        else
        {
            attractlock.SetActive(true);
        }

        if (gameStatistics.levelNumber >= LevelGenerator.lightningTowerThreshold)
        {
            lightninglock.SetActive(false);
        }
        else
        {
            lightninglock.SetActive(true);
        }
    }
}
