using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTower : Tower
{
    public Projectile Projectile;
    public float ProjectileSpeed = 5f;
    public float maxHealth = 100f;
    public float Health = 100f;
    public float shootCooldownSeconds = 3f;
    public int creditReward = 40;
    
    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shootRadius = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            // Destroy(this) only destroys the script, not the entire object
            gameStatistics.currentCredits += creditReward;
            Destroy(this.gameObject);
        }

        UpdateRangeRadius(shootRadius);
        ShowRangeIfMouseHover();
        ShootIfPossible(shootRadius, shootCooldownSeconds);

        healthBar.transform.position = transform.position + new Vector3((Health/maxHealth-1) / 2 * healthBar.GetComponent<HealthBar>().barWidth, healthBar.GetComponent<HealthBar>().height, 0);
        healthBar.transform.rotation = Quaternion.identity;
    }

    public override void Shoot(Vector3 direction)
    {
        // Vector3.back is used to change the z coordinate of the projectile so that
        // it renders on top of the tower
        Projectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized * ProjectileSpeed;
        p.OwnerTag = tag;
    }

    public override void Damage(float amount)
    {
        Health -= amount;
        healthBar.GetComponent<HealthBar>().ChangeHealth(Health/maxHealth);
    }
}
