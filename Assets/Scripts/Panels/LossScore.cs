using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LossScore : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "You Reached Level " + (gameStatistics.levelNumber).ToString();
        gameStatistics.levelNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
