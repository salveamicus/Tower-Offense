using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardUnit : MonoBehaviour
{
    public StandardProjectile Projectile;
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 moveGoal;
    private Vector3 toNormalize;
    public float speed = 0.05f;
    public float ProjectileSpeed = 0.1f;
    public float Health = 50f;
    public bool hasDirection;

    private void Start()
    {
        moveGoal = transform.position;
        hasDirection = false;
    }

    // Update is called once per frame
    void Update()
    {
        // For movement
        toNormalize = Vector3.zero;
        toNormalize.x = moveGoal.x - transform.position.x;
        toNormalize.y = moveGoal.y - transform.position.y;
        toNormalize.z = transform.position.z;
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
    }
}
