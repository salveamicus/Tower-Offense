using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorTower : Tower
{
    public float maxHealth = 200f;
    public float health = 200f;

    public float attractionStrength = 0.1f;

    public override float ShootCooldownSeconds => 1f; // Not used
    public override float ShootRadius => 3f;
    public override int CreditReward => 20;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameStatistics.currentCredits += CreditReward;
            Destroy(gameObject);
        }  

        UpdateAcceleratorCount();
        UpdateRangeRadius();
        ShowRangeIfMouseHover();

        // Attractor effect here
        foreach (GameObject unitObject in GameObject.FindGameObjectsWithTag("Unit"))
        {
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            , new Vector2(unitObject.transform.position.x, unitObject.transform.position.y));

            if (distance > ShootRadius) continue;

            Vector3 force = (transform.position - unitObject.transform.position).normalized * attractionStrength * Time.deltaTime;
            unitObject.GetComponent<Unit>().transform.position += force;
        }

        healthBar.transform.position = transform.position + new Vector3((health/maxHealth-1) / 2 * healthBar.GetComponent<HealthBar>().barWidth, healthBar.GetComponent<HealthBar>().height, 0);
        healthBar.transform.rotation = Quaternion.identity;
    }

    // Tower never shoots anything
    public override void Shoot(Vector3 direction) {}

    public override void Damage(float amount)
    {
        health -= amount;
        healthBar.GetComponent<HealthBar>().ChangeHealth(health/maxHealth);
    }

    public override void Heal(float amount)
    {
        if (health == maxHealth) return;

        health += amount;
        if (health > maxHealth) health = maxHealth;

        healthBar.GetComponent<HealthBar>().ChangeHealth(health/maxHealth);
    }
}
