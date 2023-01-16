using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCamera : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if ((Input.mousePosition.y >= Screen.height && Input.mousePosition.x >= Screen.width) || (Input.GetKey("right") && Input.GetKey("up")))
        {
            transform.position += speed * (Vector3.right + Vector3.up);
        }
        else if ((Input.mousePosition.y >= Screen.height && Input.mousePosition.x <= 0 )|| (Input.GetKey("left") && Input.GetKey("up")))
        {
            transform.position += speed * (Vector3.left + Vector3.up);
        }
        else if ((Input.mousePosition.y <= 0 && Input.mousePosition.x >= Screen.width) || (Input.GetKey("right") && Input.GetKey("down")))
        {
            transform.position += speed * (Vector3.right + Vector3.down);
        }
        else if ((Input.mousePosition.y <= 0 && Input.mousePosition.x <= 0) || ((Input.GetKey("left") && Input.GetKey("down") )))
        {
            transform.position += speed * (Vector3.left + Vector3.down);
        }
        else if (Input.mousePosition.x >= Screen.width || Input.GetKey("right"))
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
