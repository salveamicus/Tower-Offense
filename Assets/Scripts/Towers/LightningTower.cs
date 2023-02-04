using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : Tower
{
    public Projectile Projectile;
    public float MaxHealth = 150;
    public float Health = 150f;

    public override float ShootCooldownSeconds => 5f;
    public override float ShootRadius => 4f;
    public override int CreditReward => 110;

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

        UpdateAcceleratorCount();
        UpdateRangeRadius();
        ShowRangeIfMouseHover();
        ShootIfPossible();

        healthBar.transform.position = transform.position + new Vector3((Health/MaxHealth-1) / 2 * healthBar.GetComponent<HealthBar>().barWidth, healthBar.GetComponent<HealthBar>().height, 0);
        healthBar.transform.rotation = Quaternion.identity;
    }

    public override void Shoot(Vector3 direction)
    {
        // Vector3.back is used to change the z coordinate of the projectile so that
        // it renders on top of the tower
        Projectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized;
        p.OwnerTag = tag;
    }

    public override void Damage(float amount)
    {
        Health -= amount;
        healthBar.GetComponent<HealthBar>().ChangeHealth(Health/MaxHealth);
    }

    public override void Heal(float amount)
    {
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;

        healthBar.GetComponent<HealthBar>().ChangeHealth(Health/MaxHealth);
    }
}
