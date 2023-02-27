using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorTower : Tower
{
    [SerializeField] public AudioSource hitSound;

    public float maxHealth = 200f;
    public float health = 200f;

    public float attractionStrength = 2f;
    public float hurtRadius = 2f;
    public float damage = 10f;

    public override float ShootCooldownSeconds => 1f; // Not used
    public override float ShootRadius => 3f;
    public override int CreditReward => 80;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameStatistics.currentCredits += CreditReward;
            Destroy(gameObject);
        }  

        UpdateAcceleratorCount();
        UpdateRangeRadius();
        ShowRangeIfMouseHover();

        // Attractor effect here
        foreach (GameObject unitObject in GameObject.FindGameObjectsWithTag("Unit"))
        {
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            , new Vector2(unitObject.transform.position.x, unitObject.transform.position.y));

            if (distance > ShootRadius) continue;

            if (distance <= hurtRadius)
            {
                unitObject.GetComponent<Unit>().Damage(damage * Time.deltaTime);
            }

            Vector3 force = (transform.position - unitObject.transform.position).normalized * attractionStrength * Time.deltaTime;
            unitObject.GetComponent<Unit>().transform.position += force;
        }

        healthMeter.SetValue(health / maxHealth);
        healthMeter.transform.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }

    // Tower never shoots anything
    public override void Shoot(Vector3 direction) {}

    public override void Damage(float amount)
    {
        hitSound.Play();
        health -= amount;
    }

    public override void Heal(float amount)
    {
        health = Mathf.Min(maxHealth, health + amount);
    }
}
