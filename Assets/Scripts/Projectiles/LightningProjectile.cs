using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : Projectile
{
    public LineRenderer lineRenderer;

    public float LifetimeSeconds = 0.5f;
    public float ArcDistance = 1f;
    
    public int Piercing = 10;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        // Disappear after 1 second
        Invoke("Die", LifetimeSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Zap(Vector3 from, Vector3 firstTarget, string enemyTag)
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(from);
        points.Add(firstTarget);

        int pierced = 1;

        Vector2 arcPos = new Vector2(firstTarget.x, firstTarget.y);

        foreach (GameObject target in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            if (pierced > Piercing) break;

            float distance = Vector2.Distance(arcPos, new Vector2(target.transform.position.x, target.transform.position.y));

            if (distance <= ArcDistance) 
            {
                ++pierced;
                arcPos = new Vector2(target.transform.position.x, target.transform.position.y);

                points.Add(target.transform.position);

                if (enemyTag == "Tower")
                {
                    target.gameObject.GetComponent<Tower>().Damage(Damage);
                }
                else if (enemyTag == "Unit")
                {
                    target.gameObject.GetComponent<Unit>().Damage(Damage);
                }
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
