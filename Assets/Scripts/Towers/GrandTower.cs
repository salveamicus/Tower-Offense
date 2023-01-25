using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandTower : Tower
{
    public StandardProjectile standardProjectile;

    public float ProjectileSpeed = 0.2f;
    public float Health = 100f;
    public float shootRadius = 5f;
    public float shootCooldownSeconds = 4f;

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
            Debug.Log("The Grand Tower is dead -- Win Condition");
        }
    
        UpdateRangeRadius(shootRadius);
        ShowRangeIfMouseHover();
        ShootIfPossible(shootRadius, shootCooldownSeconds);
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
    }

}
