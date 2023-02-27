using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour
{
    [SerializeField] public GameObject sprite;

    // This value is equal to the x value of the scale in the inspector
    private float maxWidth;

    // Start is called before the first frame update
    void Start()
    {
        maxWidth = sprite.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // percentage must be a value between 0 and 1
    public void SetValue(float percentage)
    {
        if (percentage < 0) percentage = 0;
        sprite.transform.localScale = new Vector3(maxWidth * percentage, transform.localScale.y, transform.localScale.z);
    }
}
