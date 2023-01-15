using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : MonoBehaviour
{
    // Temporary, will later be replaced with some kind of arrow sprite
    public LineRenderer lineRenderer;
    public Vector3 velocity = Vector2.zero;

    public int sides = 5;
    public float radius = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity;

        DrawPolygon();
    }

    // Code from: https://www.youtube.com/watch?v=cRulHmoiabA
    // Draws a polygon with the line renderer
    void DrawPolygon()
    {
        const float TAU = 2 * Mathf.PI;

        lineRenderer.positionCount = sides;

        for (int i = 0; i < sides; ++i)
        {
            float rad = TAU * ((float) i / sides);
            lineRenderer.SetPosition(i, radius * new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) + transform.position);
        }

        lineRenderer.loop = true;
    }
}
