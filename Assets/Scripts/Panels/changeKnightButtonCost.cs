using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class changeKnightButtonCost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = Convert.ToString(gameStatistics.knightCost);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
