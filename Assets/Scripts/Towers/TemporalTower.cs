using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalTower : Tower
{
    public float MaxHealth = 150f;
    public float Health = 150f;

    public override float ShootCooldownSeconds => 1f; // Never used
    public override float ShootRadius => 4f;
    public override int CreditReward => 40;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            gameStatistics.currentCredits += CreditReward;
            Destroy(gameObject);
        }       

        UpdateRangeRadius();
        ShowRangeIfMouseHover();

        healthMeter.SetValue(Health / MaxHealth);
        healthMeter.transform.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }

    public override void Shoot(Vector3 direction)
    {
        // Do nothing since tower doesn't shoot
    }

    public override void Damage(float amount)
    {
        Health -= amount;
    }

    public override void Heal(float amount)
    {
        Health = Mathf.Min(MaxHealth, Health + amount);
    }
}
