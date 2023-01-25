using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public GameObject rangeSphere;
    public SpriteRenderer spriteRenderer;

    protected bool canShoot = true;

    public virtual Tuple<float, Vector3> GetClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        Vector3 closestTarget = Vector3.zero;

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Tower"))
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

        rangeSphere.SetActive(show);
    }

    public virtual void ResetCooldown()
    {
        canShoot = true;
    }

    public abstract void Shoot(Vector3 direction);
    public abstract void Damage(float amount);
}
