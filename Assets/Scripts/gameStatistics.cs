using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStatistics : MonoBehaviour
{

    // UI
    public static int currentCredits = 2000;
    public static int initialCredits = 2000;
    public static int continuousSpawnStartDelay = 30;
    public static int continuousSpawnDelay = 10;
    public static bool purchasingUnit = false;
    public static bool regeneratingLevel = false;

    // units
    public static int knightCost = 30;

    public static float fireDamage = 10f;
    public static float poisonDamage = 15f;

    // towers
    public static float placementRadius = 5f;

    void start() {
        currentCredits = initialCredits;
    }
}
