using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportUnitScript : Unit
{
    public float maxHealth = 50f;
    public float health = 50f;
    public Vector3 moveGoal;
    private Vector3 zAdjustedGoal;

    public float healAmount = 10f;
    public float healRadius = 1.5f;
    public float healCooldownSeconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        selectionCircle.SetActive(isSelected);

        // For movement
        movement(moveGoal);

        UpdateFireTime();
        UpdatePoisonTime();

        if (health <= 0) Destroy(gameObject);

        UpdateDecceleratorCount();
        UpdateRangeRadius(healRadius);
        ShowRangeIfMouseHover();

        if (canShoot) Shoot(Vector3.zero);

        // Turn towards closest tower
        Tuple<float, Vector3> target = GetClosestTarget();
        Vector3 directionVector = target.Item2 - transform.position;

        float degrees = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg + 180;
        transform.eulerAngles = Vector3.forward * degrees;

        healthBar.transform.position = transform.position + new Vector3((health/maxHealth-1)/2*0.6f, 0.4f, 0);
        healthBar.transform.rotation = Quaternion.identity;
    }

    public override void Shoot(Vector3 direction)
    {
        canShoot = false;

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (unit.transform.position == transform.position) continue;

            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            ,new Vector2(unit.transform.position.x, unit.transform.position.y));

            if (distance > healRadius) continue;

            unit.gameObject.GetComponent<Unit>().Heal(healAmount);
        }

        Invoke("ResetCooldown", healCooldownSeconds);
    }

    public override void Damage(float amount)
    {
        health -= amount;
        transform.GetChild(1).GetComponent<HealthBar>().ChangeHealth(health/maxHealth);
    }

    public override void Heal(float amount)
    {
        health = MathF.Min(health + amount, maxHealth);
        transform.GetChild(1).GetComponent<HealthBar>().ChangeHealth(health/maxHealth);
    }
}
