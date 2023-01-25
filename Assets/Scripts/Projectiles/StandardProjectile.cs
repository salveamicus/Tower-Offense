using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : MonoBehaviour
{
    public Rigidbody2D body;
    public Vector3 velocity = Vector3.zero;

    public string OwnerTag = "";
    public float Damage = 10f;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        body.isKinematic = true; // disables velocity and stuff
    }

    // Update is called once per frame
    void Update()
    {
        if (velocity != Vector3.zero)
        {
            transform.position += velocity;

            // Gets the angle to rotate the sprite by by using trig ( tan angle = y/x ).
            // Offset by 90 degrees because the sprite is facing up in the png
            // Atan2: Finds the angle of a vector whose angle = y / x
            // Rad2Deg: Equal to 180 degrees / pi Radians (Converts radians to degrees)
            float degrees = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90;

            // I have no idea wehat eulerAngles means, but if you set its value to x, it will
            // set the rotation to x degrees.
            // Vector3.forward: Shorthand for writing new Vector3(0, 0, 1) -- This is done because
            //  The sprite is being rotated about the z axis (imagine z axis as depth of the screen)
            //  so Vector3.forward * degrees is the same as: new Vector3(0, 0, degrees)
            transform.eulerAngles = Vector3.forward * degrees;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("the thingy worked");

        if (!collider.gameObject.CompareTag(OwnerTag))
        {
            Debug.Log("Hit the thingy");

            // If hit object is a unit
            if (collider.gameObject.CompareTag("Unit"))
            {
                collider.gameObject.GetComponent<StandardUnit>().Damage(Damage);
                Destroy(this.gameObject);
            }
            else if (collider.gameObject.CompareTag("Tower"))
            {
                collider.gameObject.GetComponent<StandardTower>().Damage(Damage);
                Destroy(this.gameObject);
            }
        }
    }
}
