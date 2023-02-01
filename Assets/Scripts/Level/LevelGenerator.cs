using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGenerator : MonoBehaviour
{
    // Towers to generate level with
    public GrandTower grandTower;
    public StandardTower standardTower;
    public GameObject levelNumber;

    // Generation Paramters
    public float smallestRadius = 2.5f;
    public float levelGenTime = 3f;

    // The Grand Tower of the current level
    private GrandTower currentGrandTower = null;

    private int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel(currentLevel);       
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the level is dead
        // If the level is dead, delete all towers, wait 3 seconds, and then start the next level
        if (currentGrandTower != null && currentGrandTower.Health <= 0f)
        {
            gameStatistics.regeneratingLevel = true;
            gameStatistics.currentCredits += currentGrandTower.creditReward;

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
            Vector3 pos = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0).normalized * smallestRadius;

            Instantiate(standardTower, pos, Quaternion.identity, transform);
        }

        gameStatistics.regeneratingLevel = false;
    }

    public void GenerateNextLevel()
    {
        GenerateLevel(++currentLevel);
        levelNumber.GetComponent<TextMeshProUGUI>().text = "Level " + currentLevel.ToString();
    }
}