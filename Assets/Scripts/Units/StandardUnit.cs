using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardUnit : MonoBehaviour
{
    public StandardProjectile Projectile;
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 moveGoal;
    private Vector3 toNormalize;
    private Vector3 zAdjustedGoal;
    public bool hasDirection = false;
    public float speed = 0.05f;
    public float ProjectileSpeed = 0.1f;
    float maxHealth = 50f;
    public float Health = 50f;
    public bool isSelected = false;

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
    }

    void Shoot(Vector3 direction)
    {
        StandardProjectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.velocity = direction.normalized * ProjectileSpeed;
        p.OwnerTag = tag;
    }

    public void Damage(float amount)
    {
        Health -= amount;
        transform.GetChild(1).GetComponent<HealthBar>().ChangeHealth(amount/maxHealth);
    }
}
