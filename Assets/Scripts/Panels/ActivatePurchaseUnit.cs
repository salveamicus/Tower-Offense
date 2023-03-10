using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePurchaseUnit : MonoBehaviour
{
    public void activatePurchaseUnit(bool cond) {
        gameStatistics.canPurchaseUnit = cond;
    }
}
