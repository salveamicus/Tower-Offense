using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public Rigidbody2D body;

    public string OwnerTag = "";
    public float Damage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        body.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
