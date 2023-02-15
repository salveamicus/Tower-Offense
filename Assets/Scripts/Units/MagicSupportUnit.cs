using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSupportUnit : Unit
{
    public float maxHealth = 60f;
    public float health = 60f;

    public float healAmount = 15f;
    public float healCooldownSeconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();       
        moveGoal = transform.position;
        actionRadius = 1.5f;
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

        if (canShoot) Shoot(Vector3.zero);

        healthBar.transform.position = transform.position + new Vector3((health/maxHealth-1)/2*0.6f, 0.4f, 0);
        healthBar.transform.rotation = Quaternion.identity;
    }

    public override void Shoot(Vector3 direction)
    {
        canShoot = false;

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (unit.transform.position == transform.position) continue;
            //if (!(unit.GetComponent<Unit>() is MagicUnit)) continue;

            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            ,new Vector2(unit.transform.position.x, unit.transform.position.y));

            if (distance > actionRadius) continue;

            unit.gameObject.GetComponent<Unit>().Heal(healAmount);
        }

        Invoke("ResetCooldown", healCooldownSeconds);
    }

    public override void Damage(float amount)
    {
        health -= amount;
    }

    public override void Heal(float amount)
    {
        health = System.MathF.Min(health + amount, maxHealth);
    }
}
