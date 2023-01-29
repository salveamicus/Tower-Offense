using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float barWidth;
    public float height;
    public void ChangeHealth(float ratio) {
        ratio = (ratio < 0) ? 0 : ratio; // To prevent negative health issues ~ Adam
        transform.position = new Vector3((ratio-1)/2*barWidth, transform.position.y, 0);
        transform.localScale = new Vector3(ratio*barWidth, transform.localScale.y, transform.localScale.z);
    }

}
