using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : Tower
{
    [SerializeField] public LightningProjectile Projectile;
    [SerializeField] public AudioSource hitSound;

    public float MaxHealth = 150;
    public float Health = 150f;

    public override float ShootCooldownSeconds => 2f;
    public override float ShootRadius => 8f;
    public override int CreditReward => 90;

    public float MinShootRadius => ShootRadius * 2 / 3;

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

        healthMeter.SetValue(Health / MaxHealth);
        healthMeter.transform.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }

    public override void ShootIfPossible()
    {
        if (!canShoot) return;

        Tuple<float, Vector3> target = GetClosestTarget(MinShootRadius);

        if (target.Item1 <= ShootRadius)
        {
            Shoot(target.Item2);
            canShoot = false;

            Invoke("ResetCooldown", AcceleratedCooldown);
        }
    }

    public override void Shoot(Vector3 target)
    {
        // Vector3.back is used to change the z coordinate of the projectile so that
        // it renders on top of the tower
        //Projectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        //p.Velocity = direction.normalized;
        //p.OwnerTag = tag;
        LightningProjectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.Zap(transform.position, target, "Unit");
    }

    public override void Damage(float amount)
    {
        hitSound.Play();
        Health -= amount;
    }

    public override void Heal(float amount)
    {
        Health = Mathf.Min(MaxHealth, Health + amount);
    }
}
