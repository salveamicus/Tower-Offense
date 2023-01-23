using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStatistics : MonoBehaviour
{

    // UI statistics
    public static int currentCredits = 2000;
    public static int initialCredits = 2000;
    public static int continuousSpawnStartDelay = 30;
    public static int continuousSpawnDelay = 10;

    // unit statistics
    public static int knightCost = 30;

    // tower statistics


    void start() {
        currentCredits = initialCredits;
    }
}
