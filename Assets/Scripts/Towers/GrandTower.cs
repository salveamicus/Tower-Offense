using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandTower : Tower
{
    [SerializeField] public StandardProjectile standardProjectile;
    [SerializeField] public FireProjectile fireProjectile;
    [SerializeField] public PoisonProjectile poisonProjectile;
    [SerializeField] public LightningProjectile lightningProjectile;

    public float ProjectileSpeed = 8f;
    public float MaxHealth = 100f;
    public float Health = 100f;
    public float healAmount = 0.1f;
    public float hurtRadius = 2f;
    public float attractionStrength = 2f;
    public float attractionDamage = 15f;

    public override float ShootCooldownSeconds => 2f;
    public override float ShootRadius => 10f;
    public override int CreditReward => 100;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // No need to check if health is less than 0 because the level generator 
        // will automatically check for this
    
        UpdateAcceleratorCount();
        UpdateRangeRadius();
        ShowRangeIfMouseHover();
        ShootIfPossible();

        // Support + Attract Ability
        if (gameStatistics.levelNumber >= LevelGenerator.supportTowerThreshold)
        {
            SupportNearbyTowers();
        }

        if (gameStatistics.levelNumber >= LevelGenerator.attractorTowerThreshold)
        {
            AttractNearbyUnits();
        }

        healthBar.transform.position = transform.position + new Vector3((Health/MaxHealth-1) / 2 * healthBar.GetComponent<HealthBar>().barWidth, healthBar.GetComponent<HealthBar>().height, 0);
        healthBar.transform.rotation = Quaternion.identity;
    }

    void ShootProjectile(Projectile projectile, Vector3 direction)
    {
        Projectile p = Instantiate(projectile, transform.position + Vector3.back, transform.rotation);
        p.Velocity = direction.normalized * ProjectileSpeed * ProjectileVelMultiplier;
        p.OwnerTag = tag;
    }

    void ShootStandardProjectile(Vector3 direction)
    {
        ShootProjectile(standardProjectile, direction);
    }

    void SupportNearbyTowers()
    {
        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            // Don't heal self
            if (tower.transform.position == transform.position) continue;

            Vector3 closestPoint = tower.GetComponent<Tower>().TowerBounds.ClosestPoint(transform.position);

            float distance = Vector2.Distance(transform.position
            , new Vector2(closestPoint.x, closestPoint.y));

            if (distance <= ShootRadius)
            {
                tower.gameObject.GetComponent<Tower>().Heal(healAmount);
            }
        }
    }

    void ShootFireProjectile(Vector3 direction)
    {
        ShootProjectile(fireProjectile, direction);
    }

    void ShootPoisonProjectile(Vector3 direction)
    {
        ShootProjectile(poisonProjectile, direction);
    }

    void AttractNearbyUnits()
    {
        // Attractor effect here
        foreach (GameObject unitObject in GameObject.FindGameObjectsWithTag("Unit"))
        {
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            , new Vector2(unitObject.transform.position.x, unitObject.transform.position.y));

            if (distance > ShootRadius) continue;

            if (distance <= hurtRadius)
            {
                unitObject.GetComponent<Unit>().Damage(attractionDamage * Time.deltaTime);
            }

            Vector3 force = (transform.position - unitObject.transform.position).normalized * attractionStrength * Time.deltaTime;
            unitObject.GetComponent<Unit>().transform.position += force;
        }

        healthBar.transform.position = transform.position + new Vector3((Health/MaxHealth-1) / 2 * healthBar.GetComponent<HealthBar>().barWidth, healthBar.GetComponent<HealthBar>().height, 0);
        healthBar.transform.rotation = Quaternion.identity;
    }

    void ShootLightningProjectile(Vector3 direction)
    {
        LightningProjectile p = Instantiate(lightningProjectile, transform.position + Vector3.back, Quaternion.identity);
        p.Zap(transform.position, transform.position + direction, "Unit");
    }

    // Will eventually randomly select from all available projectiles
    public override void Shoot(Vector3 direction)
    {
        canShoot = false;

        // Support Ability
        if (gameStatistics.levelNumber >= LevelGenerator.supportTowerThreshold)
        {
            SupportNearbyTowers();
        }
        
        // Projectiles
        if (gameStatistics.levelNumber >= LevelGenerator.lightningTowerThreshold)
        {
            ShootLightningProjectile(direction);
        }
        else if (gameStatistics.levelNumber >= LevelGenerator.poisonTowerThreshold)
        {
            ShootPoisonProjectile(direction);
        }
        else if (gameStatistics.levelNumber >= LevelGenerator.fireTowerThreshold)
        {
            ShootFireProjectile(direction);
        }
        else
        {
            ShootStandardProjectile(direction);
        }

        Invoke("ResetCooldown", AcceleratedCooldown);
    }

    public override void Damage(float amount)
    {
        Health -= amount;       
        transform.GetChild(1).GetComponent<HealthBar>().ChangeHealth(Health/MaxHealth);
    }

    public override void Heal(float amount)
    {
        if (Health == MaxHealth) return;

        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;

        healthBar.GetComponent<HealthBar>().ChangeHealth(Health/MaxHealth);
    }
}
