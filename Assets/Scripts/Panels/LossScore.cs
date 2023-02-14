using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LossScore : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "You Reached Level " + (gameStatistics.levelNumber).ToString();
    }
}
