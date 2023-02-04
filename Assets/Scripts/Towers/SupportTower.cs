using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTower : Tower
{
    public float maxHealth = 100f;
    public float Health = 100f;
    public float HealthAmount = 5f;

    public override float ShootCooldownSeconds => 1f;
    public override float ShootRadius => 3f;
    public override int CreditReward => 10;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            gameStatistics.currentCredits += CreditReward;
            Destroy(gameObject);
        }

        UpdateAcceleratorCount();
        UpdateRangeRadius();
        ShowRangeIfMouseHover();

        if (canShoot) Shoot(Vector3.zero);

        healthBar.transform.position = transform.position + new Vector3((Health/maxHealth-1) / 2 * healthBar.GetComponent<HealthBar>().barWidth, healthBar.GetComponent<HealthBar>().height, 0);
        healthBar.transform.rotation = Quaternion.identity;
    }

    public override void Shoot(Vector3 direction)
    {
        canShoot = false;

        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            // Don't heal self
            if (tower.transform.position == transform.position) continue;

            Vector3 closestPoint = tower.GetComponent<Tower>().TowerBounds.ClosestPoint(transform.position);

            float distance = Vector2.Distance(transform.position
            , new Vector2(closestPoint.x, closestPoint.y));

            if (distance <= ShootRadius)
            {
                tower.gameObject.GetComponent<Tower>().Heal(HealthAmount);
            }
        }

        Invoke("ResetCooldown", AcceleratedCooldown);
    }

    public override void Damage(float amount)
    {
        Health -= amount;
        healthBar.GetComponent<HealthBar>().ChangeHealth(Health/maxHealth);
    }

    public override void Heal(float amount)
    {
        Health += amount;
        if (Health > maxHealth) Health = maxHealth;

        healthBar.GetComponent<HealthBar>().ChangeHealth(Health/maxHealth);
    }
}
