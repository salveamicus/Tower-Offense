using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Rigidbody2D body;
    public Vector3 Velocity = Vector3.zero;

    public string OwnerTag = "";
    public float Damage = 10f;

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag(OwnerTag))
        {
            if (collider.gameObject.CompareTag("Unit"))
            {
                collider.gameObject.GetComponent<Unit>().Damage(Damage);
                Destroy(gameObject);
            }
            else if (collider.gameObject.CompareTag("Tower"))
            {
                collider.gameObject.GetComponent<Tower>().Damage(Damage);
                Destroy(gameObject);
            }
        }
    }

    // Remove from screen
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
