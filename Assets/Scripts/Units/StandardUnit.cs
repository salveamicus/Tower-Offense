using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardUnit : Unit
{
    public Projectile Projectile;
    private Vector3 zAdjustedGoal;
    public float speed = 0.05f;
    public float ProjectileSpeed = 3f;
    float maxHealth = 50f;
    public float Health = 50f;

    public float shootRadius = 2f;
    public float shootCooldownSeconds = 2f;

    //For animation
    public Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveGoal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        selectionCircle.SetActive(isSelected);

        // For movement
        zAdjustedGoal = Vector3.zero;
        zAdjustedGoal.x = moveGoal.x;
        zAdjustedGoal.y = moveGoal.y;
        zAdjustedGoal.z = transform.position.z;

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
        UpdateRangeRadius(shootRadius);
        ShowRangeIfMouseHover();
        ShootIfPossible(shootRadius, shootCooldownSeconds);
        
        // Turn towards closest tower
        Tuple<float, Vector3> target = GetClosestTarget();
        Vector3 directionVector = target.Item2 - transform.position;

        float degrees = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg + 180;
        transform.eulerAngles = Vector3.forward * degrees;

        // Move towards closet tower if not able to shoot anythnig
        // and not already moving
        if (target.Item1 > shootRadius && transform.position == zAdjustedGoal && target.Item1 != Mathf.Infinity)
        {
            moveGoal = target.Item2 - directionVector.normalized * shootRadius / 2;
        }

        healthBar.transform.position = transform.position + new Vector3((Health/maxHealth-1)/2*0.6f, 0.4f, 0);
        healthBar.transform.rotation = Quaternion.identity;
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
        transform.GetChild(1).GetComponent<HealthBar>().ChangeHealth(Health/maxHealth);
    }
    
    public override void Heal(float amount)
    {
        Health = MathF.Min(Health + amount, maxHealth);
        transform.GetChild(1).GetComponent<HealthBar>().ChangeHealth(Health/maxHealth);
    }
}
