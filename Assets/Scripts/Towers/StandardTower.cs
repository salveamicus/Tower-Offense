using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTower : Tower
{
    public StandardProjectile Projectile;
    public float ProjectileSpeed = 0.1f;
    public float Health = 100f;
    public float shootRadius = 3f;
    public float shootCooldownSeconds = 3f;

    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            // Destroy(this) only destroys the script, not the entire object
            Destroy(this.gameObject);
        }

        if (canShoot)
        {
            Tuple<float, Vector3> target = GetClosestTarget();

            if (target.Item1 <= shootRadius)
            {
                Shoot(target.Item2 - transform.position);
                canShoot = false;

                Invoke("ResetCooldown", shootCooldownSeconds);
            }
        }
    }

    void ResetCooldown()
    {
        canShoot = true;
    }

    void Shoot(Vector3 direction)
    {
        // Vector3.back is used to change the z coordinate of the projectile so that
        // it renders on top of the tower
        StandardProjectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.velocity = direction.normalized * ProjectileSpeed;
        p.OwnerTag = tag;
    }

    public void Damage(float amount)
    {
        Health -= amount;
    }
}
