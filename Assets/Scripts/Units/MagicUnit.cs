using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicUnit : Unit
{
    public Projectile projectile;

    private Vector3 zAdjustedGoal;

    public float projectileSpeed = 3f;

    public float maxHealth = 60f;
    public float health = 60f;

    public float shootCooldownSeconds = 0.5f;
    public float shootDeviation = 10f;

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
        Projectile p = Instantiate(projectile, transform.position + Vector3.back, transform.rotation);
        p.transform.localScale = Vector3.one * 1.5f;
        p.Velocity = Quaternion.Euler(0, 0, Random.Range(-shootDeviation, shootDeviation)) * (direction.normalized * projectileSpeed * SpeedMultiplier);
        p.OwnerTag = tag;
    }

    public override void Damage(float amount)
    {
        health -= amount;
        transform.GetChild(1).GetComponent<HealthBar>().ChangeHealth(health/maxHealth);
    }

    public override void Heal(float amount)
    {
        health = System.MathF.Min(health + amount, maxHealth);
        transform.GetChild(1).GetComponent<HealthBar>().ChangeHealth(health/maxHealth);
    }

}
