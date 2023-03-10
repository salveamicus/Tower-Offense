using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossSceneSwitch : MonoBehaviour
{
    public GameObject[] unitsonfield;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatistics.currentCredits < gameStatistics.unitCosts[0])
        {
            unitsonfield = GameObject.FindGameObjectsWithTag("Unit");
            if (unitsonfield.Length <= 0)
            {
                SceneManager.LoadScene("Loss");
                gameStatistics.currentCredits = gameStatistics.initialCredits;
                
            }
        }
    }
}
