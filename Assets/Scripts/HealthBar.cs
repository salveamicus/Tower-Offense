using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float barWidth = 0.6f;
    public float height = 1f;
    public void ChangeHealth(float ratio) {
        transform.position = new Vector3((ratio-1)/2*barWidth, transform.position.y, 0);
        transform.localScale = new Vector3(ratio*barWidth, transform.localScale.y, transform.localScale.z);
    }

}
