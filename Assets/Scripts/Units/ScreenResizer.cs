using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true, 30);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
