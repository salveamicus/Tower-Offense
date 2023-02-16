using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour
{
    [SerializeField] public SpriteRenderer sprite;

    // This value is equal to the x value of the scale in the inspector
    private float maxWidth;

    // Start is called before the first frame update
    void Start()
    {
        maxWidth = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // percentage must be a value between 0 and 1
    public void SetValue(float percentage)
    {
        transform.localScale = new Vector3(maxWidth * percentage, transform.localScale.y, transform.localScale.z);
    }
}
