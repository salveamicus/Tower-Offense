using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    float barWidth = 0.6f;
    public void ChangeHealth(float hpDrop) {
        transform.position -= new Vector3(hpDrop/2*barWidth, 0, 0);
        transform.localScale -= new Vector3(hpDrop*barWidth, 0, 0);
    }
}
