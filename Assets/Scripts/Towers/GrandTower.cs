using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandTower : MonoBehaviour
{
    public StandardProjectile standardProjectile;

    public float ProjectileSpeed = 0.5f;
    public float Health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Debug.Log("The Grand Tower is dead -- Win Condition");
        }
    }

    void ShootStandardProjectile(Vector3 direction)
    {
        StandardProjectile p = Instantiate(standardProjectile, transform.position + Vector3.back, transform.rotation);
        p.velocity = direction.normalized * ProjectileSpeed;
        p.OwnerTag = "Tower";
    }

    public void Damage(float amount)
    {
        Health -= amount;       
    }

}
