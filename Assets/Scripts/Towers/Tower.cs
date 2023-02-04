using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class to define functions for all towers
public abstract class Tower : MonoBehaviour
{
    public GameObject rangeSphere;
    public GameObject healthBar;
    public SpriteRenderer spriteRenderer;

    public abstract int CreditReward { get; }
    public abstract float ShootRadius { get; }
    public abstract float ShootCooldownSeconds { get; }

    // Function that calculates how the cooldown is affected by nearby acceleration towers
    public float AcceleratedCooldown => ShootCooldownSeconds / (accelerators + 1);
    public float ProjectileVelMultiplier => 1f + accelerators / 10f;

    public Bounds TowerBounds => spriteRenderer.bounds;

    // Display the range sphere even if the mouse is not hovering over it
    public bool rangeDisplayOverride = false;

    protected bool canShoot = true;
    protected int accelerators = 0;

    // I made this because I am not writing this function more than once
    public virtual Tuple<float, Vector3> GetClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        Vector3 closestTarget = Vector3.zero;
        
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            float dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            , new Vector2(unit.transform.position.x, unit.transform.position.y));

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestTarget = unit.transform.position;
            }
        }

        return new Tuple<float, Vector3>(closestDistance, closestTarget);
    }

    public virtual void UpdateAcceleratorCount()
    {
        accelerators = 0;

        foreach (GameObject towerObject in GameObject.FindGameObjectsWithTag("Tower"))
        {
            Tower tower = towerObject.GetComponent<Tower>();

            // Skip non acceleration towers
            if (!(tower is AccelerationTower)) continue;
            
            // If the closest point of this tower is in range of the accelerator's radius
            Vector3 closest = TowerBounds.ClosestPoint(tower.transform.position);

            float distance = Vector2.Distance(new Vector2(closest.x, closest.y)
            , new Vector2(tower.transform.position.x, tower.transform.position.y));

            if (distance <= tower.ShootRadius) ++accelerators;
        }
    }

    public virtual void ShootIfPossible()
    {
        if (!canShoot) return;

        Tuple<float, Vector3> target = GetClosestTarget();

        if (target.Item1 <= ShootRadius)
        {
            Shoot(target.Item2 - transform.position);
            canShoot = false;

            Invoke("ResetCooldown", AcceleratedCooldown);
        }
    }

    public virtual void ResetCooldown()
    {
        canShoot = true;
    }

    public virtual void UpdateRangeRadius()
    {
        rangeSphere.transform.localScale = new Vector3(ShootRadius * 2, 1, ShootRadius * 2);
    }

    public virtual void ShowRangeIfMouseHover()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool show = true;

        if (mousePos.x > transform.position.x + spriteRenderer.bounds.size.x / 2) show = false;
        if (mousePos.x < transform.position.x - spriteRenderer.bounds.size.x / 2) show = false;
        if (mousePos.y > transform.position.y + spriteRenderer.bounds.size.y / 2) show = false;
        if (mousePos.y < transform.position.y - spriteRenderer.bounds.size.y / 2) show = false;

        rangeSphere.SetActive(show || rangeDisplayOverride);
    }

    public abstract void Shoot(Vector3 direction);
    public abstract void Damage(float amount);
    public abstract void Heal(float amount);
}