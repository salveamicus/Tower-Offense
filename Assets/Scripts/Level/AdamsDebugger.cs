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

    [SerializeField] public LevelGenerator levelGenerator;
    public bool active = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)
            && Input.GetKey(KeyCode.LeftShift)
            && Input.GetKey(KeyCode.H)
            && Input.GetKey(KeyCode.A)
            && Input.GetKey(KeyCode.S)
            && Input.GetKey(KeyCode.K)
            && Input.GetKey(KeyCode.E)
            && Input.GetKey(KeyCode.L)
        )
        {
            Debug.Log("Haskell Mode Activated");
            active = true;
        }

        if (!active) return;

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

            Unit u = Instantiate(unit, pos, Quaternion.identity);
            u.isDebuggingUnit = true;
        }
    }
}
