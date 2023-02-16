using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Unit : MonoBehaviour
{

    public bool isSelected = false;
    public bool isSupport = false;

    public Vector3 moveGoal;
    public Vector3 zAdjustedGoal;

    public GameObject selectionCircle;
    public GameObject rangeSphere;
    public HealthMeter healthMeter;
    public SpriteRenderer spriteRenderer;

    public float actionRadius;

    public Bounds UnitBounds { get => spriteRenderer.bounds; }

    public float FireTime = 0;
    public float PoisonTime = 0;

    public float SpeedMultiplier => 1f / (1f + deccelerators);

    protected bool canShoot = true;
    protected float deccelerators = 0;

    public virtual Tuple<float, Vector3> GetClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        Vector3 closestTarget = Vector3.zero;

        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            // Get the closest point of the tower to the unit.
            // This allows for a more accurate targeting algorithm so that units stop moving
            // once they are in range of the closest point of the tower, meaning that they no longer target
            // the center of the tower, because for a large tower that would mean that they would have to be
            // inside the tower to start shooting
            Vector3 closestPoint = tower.gameObject.GetComponent<Tower>().TowerBounds.ClosestPoint(transform.position);

            float dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            , new Vector2(closestPoint.x, closestPoint.y));

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestTarget = closestPoint;
            }
        }

        return new Tuple<float, Vector3>(closestDistance, closestTarget);
    }

    public virtual Tuple<float, Vector3> GetClosestUnit()
    {
        float closestDistance = Mathf.Infinity;
        Vector3 closestTarget = Vector3.zero;

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (unit.gameObject.GetComponent<Unit>().isSelected) continue;

            // Get the closest point of the tower to the unit.
            // This allows for a more accurate targeting algorithm so that units stop moving
            // once they are in range of the closest point of the tower, meaning that they no longer target
            // the center of the tower, because for a large tower that would mean that they would have to be
            // inside the tower to start shooting
            Vector3 closestPoint = unit.gameObject.GetComponent<Unit>().UnitBounds.ClosestPoint(transform.position);

            float dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            , new Vector2(closestPoint.x, closestPoint.y));

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestTarget = closestPoint;
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

    public virtual void UpdateRangeRadius(float range)
    {
        rangeSphere.transform.localScale = new Vector3(range * 2, 1, range * 2);   
    }

    public virtual void ShowRangeIfMouseHover()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool show = true;

        if (mousePos.x > transform.position.x + spriteRenderer.bounds.size.x / 2) show = false;
        if (mousePos.x < transform.position.x - spriteRenderer.bounds.size.x / 2) show = false;
        if (mousePos.y > transform.position.y + spriteRenderer.bounds.size.y / 2) show = false;
        if (mousePos.y < transform.position.y - spriteRenderer.bounds.size.y / 2) show = false;

        rangeSphere.SetActive(show && !gameStatistics.purchasingUnit);
    }

    public virtual void UpdateDecceleratorCount()
    {
        deccelerators = 0;

        foreach (GameObject towerObject in GameObject.FindGameObjectsWithTag("Tower"))
        {
            Tower tower = towerObject.gameObject.GetComponent<Tower>();

            // Skip non deccelerator towers
            if (!(tower is TemporalTower)) continue;

            // If the closest point of this tower is in range of the accelerator's radius
            Vector3 closest = UnitBounds.ClosestPoint(tower.transform.position);

            float distance = Vector2.Distance(new Vector2(closest.x, closest.y)
            , new Vector2(tower.transform.position.x, tower.transform.position.y));

            if (distance <= tower.ShootRadius) ++deccelerators;
        }
    }

    public virtual void ResetCooldown()
    {
        canShoot = true;
    }

    // Apply fire damage if on fire
    public virtual void UpdateFireTime()
    {
        if (FireTime <= 0) return;
        Damage(gameStatistics.fireDamage * Time.deltaTime);

        --FireTime;
    }

    // Apply poison damage if poisoned
    public virtual void UpdatePoisonTime()
    {
        if (PoisonTime <= 0) return;
        Damage(gameStatistics.poisonDamage * Time.deltaTime);

        --PoisonTime;
    }

    public virtual void movement(Vector3 moveGoal, float speed = 1f)
    {
        Vector3 moveDirection = Vector3.zero;
        Vector3 toNormalize;
        Vector3 zAdjustedGoal;
        zAdjustedGoal = Vector3.zero;
        zAdjustedGoal.x = moveGoal.x;
        zAdjustedGoal.y = moveGoal.y;
        zAdjustedGoal.z = transform.position.z;

        if (Vector3.Distance(transform.position, zAdjustedGoal) < 0.2)
        {
            zAdjustedGoal = transform.position;
        }

        toNormalize = Vector3.zero;
        toNormalize = zAdjustedGoal - transform.position;

        moveDirection = Vector3.Normalize(toNormalize);
        transform.position += speed * Time.deltaTime * moveDirection * SpeedMultiplier; //deltatime used to anchor movement to time elapsed rather than frame count

    }

    public virtual void autoMoveGoalAndRotate()
    {
        // Turn towards closest tower
        Tuple<float, Vector3> target;
        if(isSupport)
            target = GetClosestUnit();
        else
            target = GetClosestTarget();

        Vector3 directionVector = target.Item2 - transform.position;

        float degrees = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg + 180;
        transform.eulerAngles = Vector3.forward * degrees;

        /*
        // Move towards closet tower if not able to shoot anythnig
        // and not already moving
        if (target.Item1 > actionRadius && Math.Abs(Vector3.Distance(transform.position, zAdjustedGoal)) <= 0.1 && target.Item1 != Mathf.Infinity)
        {
            moveGoal = target.Item2 - directionVector.normalized * actionRadius / 2;
        }
        */
    }

    public virtual void zAdjust()
    { 
        // For movement
        zAdjustedGoal = Vector3.zero;
        zAdjustedGoal.x = moveGoal.x;
        zAdjustedGoal.y = moveGoal.y;
        zAdjustedGoal.z = transform.position.z;
    }

    public abstract void Shoot(Vector3 direction);
    public abstract void Damage(float amount);
    public abstract void Heal(float amount);
}
