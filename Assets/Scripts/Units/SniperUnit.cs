using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperUnit : Unit
{
    public Projectile Projectile;

    public float projectileSpeed = 4f;

    public float maxHealth = 50f;
    public float health = 50f;

    public float shootCooldownSeconds = 4f;

    //For animation
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveGoal = transform.position;
        actionRadius = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        selectionCircle.SetActive(isSelected);

        zAdjust();
        autoMoveGoalAndRotate();

        //Movement animation
        animator.SetFloat("DistToTarget", Vector3.Distance(transform.position, zAdjustedGoal));

        //Reset animation attack boolean
        animator.SetBool("IsAttacking", false);

        // For movement
        movement(moveGoal);

        UpdateFireTime();
        UpdatePoisonTime();

        if (health <= 0) Destroy(gameObject);

        UpdateDecceleratorCount();
        UpdateRangeRadius(actionRadius);
        ShowRangeIfMouseHover();
        ShootIfPossible(actionRadius, shootCooldownSeconds);

        healthBar.transform.position = transform.position + new Vector3((health/maxHealth-1)/2*0.6f, 0.4f, 0);
        healthBar.transform.rotation = Quaternion.identity;
    }

    public override void Shoot(Vector3 direction)
    {
        //Attack animation
        animator.SetBool("IsAttacking", true);

        Projectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized * projectileSpeed * SpeedMultiplier;
        p.OwnerTag = tag;
    }

    public override void Damage(float amount)
    {
        health -= amount;
    }

    public override void Heal(float amount)
    {
        health = MathF.Min(health + amount, maxHealth);
    }
}
