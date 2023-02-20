using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float ExplosionRadius = 1f;
    public float ExplosionDamage = 25f;
    public float LifeTimeSeconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", LifeTimeSeconds);
    }

    public void Explode(string targetTag)
    {
        foreach (GameObject targetObject in GameObject.FindGameObjectsWithTag(targetTag))
        {
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            , new Vector2(targetObject.transform.position.x, targetObject.transform.position.y));

            if (distance <= ExplosionRadius)
            {
                if (targetTag == "Unit")
                {
                    targetObject.gameObject.GetComponent<Unit>().Damage(ExplosionDamage);
                }
                else if (targetTag == "Tower")
                {
                    targetObject.gameObject.GetComponent<Tower>().Damage(ExplosionDamage);
                }
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
