using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNumber : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "Level " + (gameStatistics.levelNumber + 1).ToString();
    }
}
