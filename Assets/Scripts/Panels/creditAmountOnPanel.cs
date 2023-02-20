using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class creditAmountOnPanel : MonoBehaviour
{
    public TMP_FontAsset p_Font;
    public bool inTutorial;

    // Start is called before the first frame update
    void Start()
    {
        if (inTutorial) {
            gameStatistics.currentCredits = gameStatistics.tutorialInitialCredits;
        } else {
            gameStatistics.currentCredits = gameStatistics.initialCredits;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = Convert.ToString(gameStatistics.currentCredits);
        gameObject.GetComponent<TextMeshProUGUI>().font = p_Font;
    }
}
