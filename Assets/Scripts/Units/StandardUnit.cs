using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardUnit : MonoBehaviour
{
    public StandardProjectile Projectile;
    public float ProjectileSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shoot(Vector3 direction)
    {
        StandardProjectile p = Instantiate(Projectile, transform.position + Vector3.back, transform.rotation);
        p.velocity = direction.normalized * ProjectileSpeed;
        p.OwnerTag = tag;
    }
}
