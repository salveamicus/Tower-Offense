using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStatistics : MonoBehaviour
{

    // UI
    public static int tutorialInitialCredits = 2000;
    public static int currentCredits = 400;
    public static int initialCredits = 400;
    public static int continuousSpawnStartDelay = 30;
    public static int continuousSpawnDelay = 10;
    public static int levelNumber = 1;
    public static bool purchasingUnit = false;
    public static bool regeneratingLevel = false;
    public static int towersPerLevel = 3;

    // units
    public static int[] unitCosts = {75, 100, 200, 325, 350, 450, 750}; // knight, sniper, support, demo, mage, cultist, heavy in order

    public static float fireDamage = 10f;
    public static float poisonDamage = 15f;

    // towers
    public static float placementRadius = 5f;

}
