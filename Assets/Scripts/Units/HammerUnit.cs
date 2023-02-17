using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerUnit : Unit
{
    public Projectile projectile;

    public float ProjectileSpeed = 1f;
    public float maxHealth = 80f;
    public float health = 80f;

    public float shootCooldownSeconds = 4f;

    // Slower than normal movement speed
    public override float SpeedMultiplier => 1f / (1.5f * (1f + deccelerators));

    // Start is called before the first frame update
    void Start()
    {
        actionRadius = 3f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveGoal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        selectionCircle.SetActive(isSelected);

        zAdjust();
        autoMoveGoalAndRotate();
        movement(moveGoal);

        UpdateFireTime();
        UpdatePoisonTime();

        if (health <= 0) Destroy(gameObject);

        UpdateDecceleratorCount();
        UpdateRangeRadius(actionRadius);
        ShowRangeIfMouseHover();
        ShootIfPossible(actionRadius, shootCooldownSeconds);

        healthMeter.SetValue(health / maxHealth);
        healthMeter.transform.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }

    public override void Shoot(Vector3 direction)
    {
        Projectile p = Instantiate(projectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized * ProjectileSpeed * SpeedMultiplier;
        p.OwnerTag = tag;
    }

    public override void Damage(float amount)
    {
        health -= amount;
    }
    
    public override void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }
}
