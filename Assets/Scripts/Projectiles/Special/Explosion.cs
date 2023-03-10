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

    public void Explode()
    {
        foreach (GameObject targetObject in GameObject.FindGameObjectsWithTag("Tower"))
        {
            Tower tower = targetObject.GetComponent<Tower>();
            Vector3 closestPoint = tower.ClosestPoint(transform.position);

            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            , new Vector2(closestPoint.x, closestPoint.y));

            if (distance <= ExplosionRadius)
            {
                targetObject.gameObject.GetComponent<Tower>().Damage(ExplosionDamage);
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
