using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower
{
    public Projectile Projectile;
    public float ProjectileSpeed = 7f;
    public float MaxHealth = 120f;
    public float Health = 120f;

    public override float ShootCooldownSeconds => 2f;
    public override float ShootRadius => 4f;
    public override int CreditReward => 50;

    // How far each shot in multi shot differs
    public float ShootDeviationDegrees = 7f;

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
        float currentDeviation = -ShootDeviationDegrees;

        for (int i = 0; i < 3; ++i)
        {
            // Vector3.back is used to change the z coordinate of the projectile so that
            // it renders on top of the tower
            Projectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
            p.Velocity = Quaternion.Euler(0, 0, currentDeviation) * (direction.normalized * ProjectileSpeed * ProjectileVelMultiplier);
            p.OwnerTag = tag;

            currentDeviation += ShootDeviationDegrees;
        }
    }

    public override void Damage(float amount)
    {
        Health -= amount;
        healthBar.GetComponent<HealthBar>().ChangeHealth(Health/MaxHealth);
    }

    public override void Heal(float amount)
    {
        if (Health == MaxHealth) return;

        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;

        healthBar.GetComponent<HealthBar>().ChangeHealth(Health/MaxHealth);
    }

}
