using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower
{
    public Projectile Projectile;
    public float ProjectileSpeed = 1f;
    public float MaxHealth = 120f;
    public float Health = 120f;

    // Anything within this radius of the tower has their health slowly drained away
    public float ToxicRadius = 2f;
    public float ToxicDamage = 10f;

    public override float ShootCooldownSeconds => 5f;
    public override float ShootRadius => 3f;
    public override int CreditReward => 30;

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

        // Slowly damage everything in the ToxicRadius
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(unit.transform.position.x, unit.transform.position.y)) <= ToxicRadius)
            {
                unit.gameObject.GetComponent<Unit>().Damage(ToxicDamage * Time.deltaTime);
            }
        }

        healthMeter.SetValue(Health / MaxHealth);
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
        Health = Mathf.Min(MaxHealth, Health + amount);
    }
}
