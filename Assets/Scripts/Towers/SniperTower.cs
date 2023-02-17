using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : Tower
{
    public Projectile Projectile;
    public float ProjectileSpeed = 5f;
    public float maxHealth = 90;
    public float Health = 90;
    public float shootCooldownSeconds = 3f;
    public int creditReward = 20;

    public override float ShootCooldownSeconds => 5;
    public override float ShootRadius => 10f;
    public override int CreditReward => 20;

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
            Destroy(this.gameObject);
        }

        UpdateAcceleratorCount();
        UpdateRangeRadius();
        ShowRangeIfMouseHover();
        ShootIfPossible();

        healthMeter.SetValue(Health / maxHealth);
        healthMeter.transform.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }

    public override void Shoot(Vector3 direction)
    {
        // Vector3.back is used to change the z coordinate of the projectile so that
        // it renders on top of the tower
        Projectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized * ProjectileSpeed * ProjectileVelMultiplier;
        p.OwnerTag = tag;
    }

    public override void Damage(float amount)
    {
        Health -= amount;
    }

    public override void Heal(float amount)
    {
        Health = Mathf.Min(maxHealth, Health + amount);
    }
}
