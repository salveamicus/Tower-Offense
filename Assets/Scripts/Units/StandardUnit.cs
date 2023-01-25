using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardUnit : Unit
{
    public Projectile Projectile;
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 moveGoal;
    private Vector3 toNormalize;
    private Vector3 zAdjustedGoal;
    public bool hasDirection = false;
    public float speed = 0.05f;
    public float ProjectileSpeed = 0.1f;
    public float Health = 50f;
    public bool isSelected = false;

    public float shootRadius = 1f;
    public float shootCooldownSeconds = 2f;

    private void Start()
    {
        moveGoal = transform.position;
        hasDirection = false;
    }

    // Update is called once per frame
    void Update()
    {
        // For movement
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
        transform.position += speed * moveDirection;

        //Debug.Log("Health: " + Health + ", Position: " + transform.position); //use this is you need to debug movement or health 

        if (Health <= 0)
        {
            // Destroy(this) only destroys the script, not the entire object
            Destroy(this.gameObject);
        }

        ShootIfPossible(shootRadius, shootCooldownSeconds);
    }

    public override void Shoot(Vector3 direction)
    {
        Projectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized * ProjectileSpeed;
        p.OwnerTag = tag;
    }

    public override void Damage(float amount)
    {
        Health -= amount;
    }
}
