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
                Unit unit = collider.gameObject.GetComponent<Unit>();
                unit.Damage(Damage);
                OnHitUnit(unit);
                Destroy(gameObject);
            }
            else if (collider.gameObject.CompareTag("Tower"))
            {
                Tower tower = collider.gameObject.GetComponent<Tower>();
                tower.Damage(Damage);
                OnHitTower(tower);
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnHitUnit(Unit unit) {}
    protected virtual void OnHitTower(Tower tower) {}

    // Remove from screen
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
