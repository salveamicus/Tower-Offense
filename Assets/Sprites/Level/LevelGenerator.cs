using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Towers to generate level with
    public GrandTower grandTower;
    public StandardTower standardTower;

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
    }
}