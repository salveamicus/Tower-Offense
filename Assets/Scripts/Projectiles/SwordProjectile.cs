using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public Rigidbody2D body;

    public string OwnerTag = "";
    public float Damage = 10f;
    public float LifetimeSeconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        body.isKinematic = true;

        Invoke("Die", LifetimeSeconds);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
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
    void Die()
    {
        Destroy(gameObject);
    }
}
