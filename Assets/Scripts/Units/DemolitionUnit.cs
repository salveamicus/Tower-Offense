using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemolitionUnit : Unit
{
    [SerializeField] public Projectile projectile;
    [SerializeField] public AudioSource launchSound;
    [SerializeField] public AudioSource hitSound;

    public float projectileSpeed = 1f;

    public float maxHealth = 80f;
    public float health = 120f;
    public float speed = 1f;

    public float shootCooldownSeconds = 5f;

    //animation
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveGoal = transform.position;
        actionRadius = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        selectionCircle.SetActive(isSelected);

        zAdjust();
        autoMoveGoalAndRotate();

        // For movement
        movement(moveGoal, speed);

        // Animate movement
        animator.SetFloat("DistToTarget", Vector3.Distance(transform.position, zAdjustedGoal));

        // Reset attack animation bool
        animator.SetBool("IsAttacking", false);

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
        // Animate attack
        animator.SetBool("IsAttacking", true);

        launchSound.Play();

        Projectile p = Instantiate(projectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized * projectileSpeed * SpeedMultiplier;
        p.OwnerTag = tag;
    }

    public override void Damage(float amount)
    {
        hitSound.Play();
        health -= amount;
    }

    public override void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }
}
