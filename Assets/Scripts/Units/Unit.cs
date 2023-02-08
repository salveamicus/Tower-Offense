using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public GameObject rangeSphere;
    public SpriteRenderer spriteRenderer;

    public Bounds UnitBounds { get => spriteRenderer.bounds; }

    protected bool canShoot = true;

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

    public virtual void ResetCooldown()
    {
        canShoot = true;
    }

    public abstract void Shoot(Vector3 direction);
    public abstract void Damage(float amount);
}
