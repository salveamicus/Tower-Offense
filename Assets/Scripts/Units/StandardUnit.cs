using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardUnit : Unit
{
    public Projectile Projectile;
    public float ProjectileSpeed = 3f;
    float maxHealth = 50f;
    public float Health = 50f;

    public float shootCooldownSeconds = 2f;

    //For animation
    public Animator animator;

    private void Start()
    {
        actionRadius = 2f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveGoal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        selectionCircle.SetActive(isSelected);

        zAdjust();
        autoMoveGoalAndRotate();

        //For calculating if movement spritesheet should animate
        animator.SetFloat("DistToTarget", Vector3.Distance(transform.position, zAdjustedGoal));
        
        //Reset attack sprite animation boolean
        animator.SetBool("IsAttacking", false);

        movement(moveGoal);

        //Debug.Log("Health: " + Health + ", Position: " + transform.position); //use this is you need to debug movement or health 

        UpdateFireTime();
        UpdatePoisonTime();

        if (Health <= 0)
        {
            // Destroy(this) only destroys the script, not the entire object
            Destroy(this.gameObject);
        }

        UpdateDecceleratorCount();
        UpdateRangeRadius(actionRadius);
        ShowRangeIfMouseHover();
        ShootIfPossible(actionRadius, shootCooldownSeconds);

        healthMeter.SetValue(Health / maxHealth);
        healthMeter.transform.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }

    public override void Shoot(Vector3 direction)
    {
        //Play attack sprite animation
        animator.SetBool("IsAttacking", true);

        Projectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized * ProjectileSpeed * SpeedMultiplier;
        p.OwnerTag = tag;
    }

    public override void Damage(float amount)
    {
        Health -= amount;
    }
    
    public override void Heal(float amount)
    {
        Health = MathF.Min(Health + amount, maxHealth);
    }
}
