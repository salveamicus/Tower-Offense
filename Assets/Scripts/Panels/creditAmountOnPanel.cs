using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class creditAmountOnPanel : MonoBehaviour
{
    public TMP_FontAsset p_Font;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = Convert.ToString(gameStatistics.currentCredits);
        gameObject.GetComponent<TextMeshProUGUI>().font = p_Font;
    }
}
