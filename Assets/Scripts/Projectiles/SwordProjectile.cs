using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : Projectile
{
    public float LifetimeSeconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        body.isKinematic = true;

        // Despawn if never hits anything
        Invoke("Die", LifetimeSeconds);
    }

    void Update()
    {
        if (Velocity != Vector3.zero)
        {
            transform.position = transform.position + Velocity * Time.deltaTime;

            float degrees = Mathf.Atan2(Velocity.y, Velocity.x) * Mathf.Rad2Deg + 90;
            transform.eulerAngles = Vector3.forward * degrees;
        }
    }
}
