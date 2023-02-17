using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamsDebugger : MonoBehaviour
{
    [SerializeField] public StandardUnit standardUnit;
    [SerializeField] public SniperUnit sniperUnit;
    [SerializeField] public SupportUnitScript supportUnit;
    [SerializeField] public MagicUnit magicUnit;
    [SerializeField] public MagicSupportUnit magicSupportUnit;
    [SerializeField] public DemolitionUnit demolitionUnit;
    [SerializeField] public HammerUnit hammerUnit;

    public LevelGenerator levelGenerator;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            levelGenerator.KillGrandTower();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnUnit(standardUnit, (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 50 : 1);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            SpawnUnit(sniperUnit, (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 50 : 1);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            SpawnUnit(supportUnit, (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 50 : 1);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnUnit(magicUnit, (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 50 : 1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnUnit(magicSupportUnit, (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 50 : 1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnUnit(demolitionUnit, (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 50 : 1);
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnUnit(hammerUnit, (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 50 : 1);
        }
    }

    void SpawnUnit(Unit unit, int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.x += Random.Range(-0.5f, 0.5f);
            pos.y += Random.Range(-0.5f, 0.5f);
            pos.z = 0;

            Instantiate(unit, pos, Quaternion.identity);
        }
    }
}
