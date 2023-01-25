using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class to define functions for all towers
public abstract class Tower : MonoBehaviour
{
    protected bool canShoot = true;

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

    public virtual void ShootIfPossible(float radius, float cooldown)
    {
        if (!canShoot) return;

        Tuple<float, Vector3> target = GetClosestTarget();

        if (target.Item1 <= radius)
        {
            Shoot(target.Item2 - transform.position);
            canShoot = false;

            Invoke("ResetCooldown", cooldown);
        }
    }

    public virtual void ResetCooldown()
    {
        canShoot = true;
    }

    public abstract void Shoot(Vector3 direction);
    public abstract void Damage(float amount);
}