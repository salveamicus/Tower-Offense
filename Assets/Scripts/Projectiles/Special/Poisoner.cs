using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoner : MonoBehaviour
{
    public SpriteRenderer sprite;

    public float SpriteScaling = 3f;
    public float PoisonRadius = 1.5f;
    public float LifeTimeSeconds = 3f;
    public float PoisonTime = 100f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(SpriteScaling, SpriteScaling, 1);

        Invoke("Die", LifeTimeSeconds);   
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure rendered under everything
        if (transform.position.z != 1) transform.position = new Vector3(transform.position.x, transform.position.y, 1);

        // Rotate it to make some kind of animation   
        transform.rotation = Quaternion.Euler(0, 0, 1) * transform.rotation;

        // Effect all units around it that don't have poison effect
        foreach (GameObject unitObject in GameObject.FindGameObjectsWithTag("Unit"))
        {
            Unit unit = unitObject.gameObject.GetComponent<Unit>();

            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
            , new Vector2(unit.transform.position.x, unit.transform.position.y));

            if (distance <= PoisonRadius && unit.PoisonTime < 3)
            {
                unit.PoisonTime += PoisonTime;
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
