using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Towers to generate level with
    public GrandTower grandTower;
    public StandardTower standardTower;

    // Generation Paramters
    public float smallestRadius = 2.5f;

    // The Grand Tower of the current level
    private GrandTower currentGrandTower = null;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel(0);       
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}