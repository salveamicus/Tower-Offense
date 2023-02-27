using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStatistics : MonoBehaviour
{

    // UI
    public static int tutorialInitialCredits = 2000;
    public static int currentCredits = 450;
    public static int initialCredits = 450;
    public static int continuousSpawnStartDelay = 30;
    public static int continuousSpawnDelay = 10;
    public static int levelNumber = 1;
    public static bool purchasingUnit = false;
    public static bool regeneratingLevel = false;

    // units
    public static int[] unitCosts = {75, 100, 200, 250, 300, 350, 600}; // knight, sniper, support, mage, demo, cultist, heavy in order

    public static float fireDamage = 10f;
    public static float poisonDamage = 15f;

    // towers
    public static float placementRadius = 5f;

}
