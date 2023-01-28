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

    // units
    public static int knightCost = 30;
    public static List<GameObject> units = new List<GameObject>();

    // towers
    public static List<GameObject> towers = new List<GameObject>();
    public static float placementRadius = 5f;

    void start() {
        currentCredits = initialCredits;
    }
}
