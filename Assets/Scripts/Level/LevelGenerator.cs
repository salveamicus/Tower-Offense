using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public float ringSize = 0.5f;
    public float levelGenTime = 3f;

    // The Grand Tower of the current level
    private GrandTower currentGrandTower = null;

    private int currentLevel = 0;

    private string dna = "";

    // Start is called before the first frame update
    void Start()
    {
        //GenerateLevel(currentLevel);
        GenerateLevelFromDNA("LLLL/FFFF/PPPP/UUUU/SSSS");
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the level is dead
        // If the level is dead, delete all towers, wait 3 seconds, and then start the next level
        if (currentGrandTower != null && currentGrandTower.Health <= 0f)
        {
            gameStatistics.regeneratingLevel = true;
            gameStatistics.currentCredits += currentGrandTower.CreditReward;

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            Invoke("GenerateNextLevel", levelGenTime);
        }
    }

    void RemoveAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // For now, only places X amount of standard towers in a circular formation around it
    // where X = levelNumber
    // This will later be modified so that once space runs out in the circle it will start
    // upgrading towers and placing new ones in outer circles
    // Newer algorithm will also modify the stats of existing towers
    public void GenerateLevel(int levelNumber)
    {
        RemoveAllChildren();

        currentGrandTower = Instantiate(grandTower, transform.position, Quaternion.identity, transform);       

        int numTowers = levelNumber; // This will change eventually to only make a new tower every X levels
        float angle = 360f / numTowers;
        float startingAngle = -90; // This will change eventually to be a random number

        for (int i = 0; i < levelNumber; ++i)
        {
            float radians = (startingAngle + angle * i) * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0).normalized * smallestRing;

            Instantiate(standardTower, pos, Quaternion.identity, transform);
        }

        gameStatistics.regeneratingLevel = false;
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
    public void GenerateLevelFromDNA(string newDNA)
    {
        RemoveAllChildren();

        currentGrandTower = Instantiate(grandTower, transform.position, Quaternion.identity, transform);

        List<char> currentTowers = new List<char>();
        int ring = 1; // Which ring to spawn current set of towers at

        foreach (char symbol in newDNA)
        {
            if (symbol == '/')
            {
                currentTowers.Add(symbol);
            }
            else
            {
                float angle = 360f / currentTowers.Count;
                float startingAngle = -90; // North of the grand tower

                for (int i = 0; i < currentTowers.Capacity; ++i)
                {
                    float radians = (startingAngle + angle * i);
                    Vector3 pos = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0).normalized * (smallestRing + ringSize * i);
                    
                    switch (currentTowers[i])
                    {
                    case 'N': 
                        Instantiate(sniperTower, pos, Quaternion.identity, transform);
                        break;
                    case 'U': 
                        Instantiate(supportTower, pos, Quaternion.identity, transform);
                        break;
                    case 'A': 
                        Instantiate(accelerationTower, pos, Quaternion.identity, transform);
                        break;
                    case 'F': 
                        Instantiate(fireTower, pos, Quaternion.identity, transform);
                        break;
                    case 'P': 
                        Instantiate(poisonTower, pos, Quaternion.identity, transform);
                        break;
                    case 'T': 
                        Instantiate(temporalTower, pos, Quaternion.identity, transform);
                        break;
                    case 'C': 
                        Instantiate(attractorTower, pos, Quaternion.identity, transform);
                        break;
                    case 'L': 
                        Instantiate(sniperTower, pos, Quaternion.identity, transform);
                        break;
                    default: // Standard tower if unrecognized symbol
                        Instantiate(standardTower, pos, Quaternion.identity, transform);
                        break;
                    }
                }

                currentTowers.Clear();
                ++ring;
            }
        }

        dna = newDNA;
    }

    public void GenerateNextLevel()
    {
        GenerateLevel(++currentLevel);
        gameStatistics.levelNumber = this.currentLevel;
    }
}