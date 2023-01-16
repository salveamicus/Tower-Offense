using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCamera : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        Debug.Log("Screen Height : " + Screen.height);
        Debug.Log("Screen Width : " + Screen.width);
        Debug.Log("Mouse Position : (" + Input.mousePosition.x + ", " + Input.mousePosition.y + ")");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x >= Screen.width || Input.GetKey("right"))
        {
            transform.position += speed * Vector3.right;
        } 
        else if (Input.mousePosition.x <= 0 || Input.GetKey("left"))
        {
            transform.position += speed * Vector3.left;
        }
        else if (Input.mousePosition.y <= 0 || Input.GetKey("down"))
        {
            transform.position += speed * Vector3.down;
        }
        else if (Input.mousePosition.y >= Screen.height || Input.GetKey("up"))
        {
            transform.position += speed * Vector3.up;
        }
    }
}
