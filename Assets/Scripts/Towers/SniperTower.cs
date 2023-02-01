using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : Tower
{
    public Projectile Projectile;
    public float ProjectileSpeed = 5f;
    public float maxHealth = 100f;
    public float Health = 100f;
    public float shootCooldownSeconds = 3f;
    public int creditReward = 40;

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
            Destroy(this.gameObject);
        }

        UpdateRangeRadius();
        ShowRangeIfMouseHover();
    }
}
