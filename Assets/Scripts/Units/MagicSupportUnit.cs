using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSupportUnit : Unit
{
    [SerializeField] public AudioSource hitSound;

    public float maxHealth = 60f;
    public float health = 80f;

    public float healAmount = 15f;
    public float healCooldownSeconds = 1f;

    //For animation
    public Animator animator;

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

        // Animate movement
        animator.SetFloat("DistToTarget", Vector3.Distance(transform.position, zAdjustedGoal));

        UpdateFireTime();
        UpdatePoisonTime();

        if (health <= 0) Destroy(gameObject);

        UpdateDecceleratorCount();
        UpdateRangeRadius(actionRadius);
        ShowRangeIfMouseHover();

        if (canShoot) Shoot(Vector3.zero);

        healthMeter.SetValue(health / maxHealth);
        healthMeter.transform.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
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
        hitSound.Play();
        health -= amount;
    }

    public override void Heal(float amount)
    {
        health = System.MathF.Min(health + amount, maxHealth);
    }
}
