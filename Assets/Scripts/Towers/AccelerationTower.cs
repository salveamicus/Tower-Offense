using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationTower : Tower
{
    [SerializeField] public AudioSource hitSound;

    public float MaxHealth = 70f;
    public float Health = 70f;

    public override float ShootCooldownSeconds => 1f; // Never used
    public override float ShootRadius => 3f;
    public override int CreditReward => 40;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            gameStatistics.currentCredits += CreditReward;
            Destroy(gameObject);
        }   

        UpdateRangeRadius();
        ShowRangeIfMouseHover();

        healthMeter.SetValue(Health / MaxHealth);
        healthMeter.transform.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }

    public override void Shoot(Vector3 direction)
    {
        // Do nothing since tower doesn't shoot
    }

    public override void Damage(float amount)
    {
        hitSound.Play();
        Health -= amount;
    }

    public override void Heal(float amount)
    {
        Health = Mathf.Min(MaxHealth, Health + amount);
    }
}
