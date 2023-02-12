using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandTower : Tower
{
    public StandardProjectile standardProjectile;

    public float ProjectileSpeed = 8f;
    public float MaxHealth = 100f;
    public float Health = 100f;

    public override float ShootCooldownSeconds => 4f;
    public override float ShootRadius => 10f;
    public override int CreditReward => 100;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // No need to check if health is less than 0 because the level generator 
        // will automatically check for this
    
        UpdateRangeRadius();
        ShowRangeIfMouseHover();
        ShootIfPossible();
    }

    void ShootStandardProjectile(Vector3 direction)
    {
        StandardProjectile p = Instantiate(standardProjectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized * ProjectileSpeed;
        p.OwnerTag = "Tower";
    }

    // Will eventually randomly select from all available projectiles
    public override void Shoot(Vector3 direction)
    {
        ShootStandardProjectile(direction);
    }

    public override void Damage(float amount)
    {
        Health -= amount;       
        transform.GetChild(1).GetComponent<HealthBar>().ChangeHealth(Health/MaxHealth);
    }

    public override void Heal(float amount)
    {
        if (Health == MaxHealth) return;

        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;

        healthBar.GetComponent<HealthBar>().ChangeHealth(Health/MaxHealth);
    }
}
