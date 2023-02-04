using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : Projectile
{
    public LineRenderer lineRenderer;
    public float LifetimeSeconds = 0.5f;
    public float ArcDistance = 1f;

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

        foreach (GameObject target in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            float distance = Vector2.Distance(new Vector2(firstTarget.x, firstTarget.y)
            , new Vector2(target.transform.position.x, target.transform.position.y));

            if (distance <= ArcDistance) 
            {
                Debug.Log("this ran");
                points.Add(target.transform.position);
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
