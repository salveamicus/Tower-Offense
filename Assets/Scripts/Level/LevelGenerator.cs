using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Towers to generate level with
    public GrandTower grandTower;
    public StandardTower standardTower;
    public SniperTower sniperTower;
    public SupportTower supportTower;
    public AccelerationTower accelerationTower;
    public FireTower fireTower;
    public PoisonTower poisonTower;
    public TemporalTower temporalTower;
    public AttractorTower attractorTower;
    public LightningTower lightningTower;

    public GameObject levelNumber;

    // Generation Paramters
    public float smallestRing = 2.5f;
    public float innerRingCapacity = 8f;
    public float ringCapacityMultiplier = 1.5f;
    public float ringSize = 2f;
    public float levelGenTime = 3f;

    // The level number for each tower to start spawning
    public const int sniperTowerThreshold = 5;
    public const int supportTowerThreshold = 10;
    public const int accelerationTowerThreshold = 15;
    public const int fireTowerThreshold = 20;
    public const int poisonTowerThreshold = 25;
    public const int temporalTowerThreshold = 30;
    public const int attractorTowerThreshold = 35;
    public const int lightningTowerThreshold = 40;

    // The Grand Tower of the current level
    private GrandTower currentGrandTower = null;

    private int currentLevel = 0;

    private string dna = "";

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevelFromDNA();
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the level is dead
        // If the level is dead, delete all towers, wait 3 seconds, and then start the next level
        if (currentGrandTower != null && currentGrandTower.Health <= 0f)
        {
            gameStatistics.regeneratingLevel = true;

            foreach (Transform child in transform)
            {
                gameStatistics.currentCredits += child.gameObject.GetComponent<Tower>().CreditReward;
                Destroy(child.gameObject);
            }

            Invoke("GenerateNextLevel", levelGenTime);
        }
        else gameStatistics.regeneratingLevel = false;
    }

    void RemoveAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    char GetRandomTowerSymbol()
    {
        switch (currentLevel)
        {
        case sniperTowerThreshold       : return 'N';
        case supportTowerThreshold      : return 'U';
        case accelerationTowerThreshold : return 'A';
        case fireTowerThreshold         : return 'F';
        case poisonTowerThreshold       : return 'P';
        case temporalTowerThreshold     : return 'T';
        case attractorTowerThreshold    : return 'C';
        case lightningTowerThreshold    : return 'L';
        }

        char[] choices = {'S', 'N', 'U', 'A', 'F', 'P', 'T', 'C', 'L'};

        if (currentLevel < sniperTowerThreshold) return 'S';
        else if (currentLevel < supportTowerThreshold) return choices[Random.Range(0, 2)];
        else if (currentLevel < accelerationTowerThreshold) return choices[Random.Range(0, 3)];
        else if (currentLevel < fireTowerThreshold) return choices[Random.Range(0, 4)];
        else if (currentLevel < poisonTowerThreshold) return choices[Random.Range(0, 5)];
        else if (currentLevel < temporalTowerThreshold) return choices[Random.Range(0, 6)];
        else if (currentLevel < attractorTowerThreshold) return choices[Random.Range(0, 7)];
        else if (currentLevel < lightningTowerThreshold) return choices[Random.Range(0, 8)];
    
        return choices[Random.Range(0, choices.Length)];
    }

    void MutateDNA()
    {
        ++currentLevel;

        int currentRing = dna.Length - dna.Replace("/", "").Length;

        try
        {
            string outerRing = dna.Substring(0, dna.Length - 1);
            if (outerRing.LastIndexOf('/') != -1) outerRing = outerRing.Substring(outerRing.LastIndexOf('/'));

            if (outerRing.Length < innerRingCapacity * currentRing * ringCapacityMultiplier)
            {
                dna = dna.Substring(0, dna.Length - 1) + GetRandomTowerSymbol() + "/";
            }
            else
            {
                dna += GetRandomTowerSymbol() + "/";
            }
        }
        catch
        {
            dna = dna.Substring(0, Mathf.Max(0, dna.Length - 1)) + GetRandomTowerSymbol() + "/";
        }
    }

    // Slash indicates the next circle out
    // S - Standard Tower
    // N - Sniper Tower
    // U - Support Tower
    // A - Acceleration Tower
    // F - Fire Tower
    // P - Poison Tower
    // T - Temporal Tower
    // C - Attractor Tower
    // L - Lightning Tower
    public void GenerateLevelFromDNA()
    {
        RemoveAllChildren();
        MutateDNA();

        Debug.Log("Level DNA: " + dna);

        // Increase Grand Tower health per level
        currentGrandTower = Instantiate(grandTower, transform.position, Quaternion.identity, transform);
        currentGrandTower.MaxHealth *= 1 + (currentLevel / 10f);
        currentGrandTower.Health = currentGrandTower.MaxHealth;

        List<char> currentTowers = new List<char>();
        int ring = 1; // Which ring to spawn current set of towers at

        foreach (char symbol in dna)
        {
            if (symbol != '/')
            {
                currentTowers.Add(symbol);
            }
            else
            {
                float angle = 360f / currentTowers.Count;
                float startingAngle = -90; // North of the grand tower

                for (int i = 0; i < currentTowers.Count; ++i)
                {
                    float degrees = (startingAngle + angle * i);
                    Quaternion towerAngle = Quaternion.Euler(0, 0, degrees);
                    Quaternion towerOrientation = Quaternion.Euler(0, 0, degrees - 90);

                    //Vector3 pos = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0).normalized * (smallestRing + i * ringSize);
                    //Vector3 pos = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0).normalized * smallestRing;
                    Vector3 pos = towerAngle * new Vector3(smallestRing + ring * ringSize, 0, 0);
                    
                    System.Func<Tower, Tower> spawn = (Tower t) => Instantiate(t, pos, towerOrientation, transform);
                    
                    switch (currentTowers[i])
                    {
                    case 'N': spawn(sniperTower);       break;
                    case 'U': spawn(supportTower);      break;
                    case 'A': spawn(accelerationTower); break;
                    case 'F': spawn(fireTower);         break;
                    case 'P': spawn(poisonTower);       break;
                    case 'T': spawn(temporalTower);     break;
                    case 'C': spawn(attractorTower);    break;
                    case 'L': spawn(lightningTower);    break;
                    default: // Standard tower if unrecognized symbol
                        spawn(standardTower);
                        break;
                    }
                }

                currentTowers.Clear();
                ++ring;
            }
        }
    }

    public void GenerateNextLevel()
    {
        //GenerateLevel(++currentLevel);
        GenerateLevelFromDNA();
        gameStatistics.levelNumber = this.currentLevel;

        //Unit conversion to credits at end of each level
        GameObject[] allUnits = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject obj in allUnits)
        {
            switch (obj.gameObject.GetComponent<Unit>())
            {
                case StandardUnit:
                    gameStatistics.currentCredits += gameStatistics.unitCosts[0];
                    break;

                case SniperUnit:
                    gameStatistics.currentCredits += gameStatistics.unitCosts[1];
                    break;

                case SupportUnitScript:
                    gameStatistics.currentCredits += gameStatistics.unitCosts[2];
                    break;

                case DemolitionUnit:
                    gameStatistics.currentCredits += gameStatistics.unitCosts[3];
                    break;

                case MagicUnit:
                    gameStatistics.currentCredits += gameStatistics.unitCosts[4];
                    break;

                case MagicSupportUnit:
                    gameStatistics.currentCredits += gameStatistics.unitCosts[5];
                    break;

                default:
                    break;
            }
            Destroy(obj);
        }
    }

    public void KillGrandTower()
    {
        currentGrandTower.Health = -Mathf.Infinity;
    }
}