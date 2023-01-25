using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCamera : MonoBehaviour
{
    public float panSpeed = 0.1f;
    public float scrollSpeed = 0.1f;

    private bool _isDraggingMouseBox = false;
    private Vector3 _dragStartPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDraggingMouseBox = true;
            _dragStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
            _isDraggingMouseBox = false;

        if (Input.mouseScrollDelta.y > 0)
        {
            Camera.main.orthographicSize -= scrollSpeed;
            Camera.main.nearClipPlane -= scrollSpeed;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            Camera.main.orthographicSize += scrollSpeed;
            Camera.main.farClipPlane += scrollSpeed;
        }

        if ((Input.mousePosition.y >= Screen.height && Input.mousePosition.x >= Screen.width) || (Input.GetKey("right") && Input.GetKey("up")))
        {
            transform.position += panSpeed * (Vector3.right + Vector3.up);
        }
        else if ((Input.mousePosition.y >= Screen.height && Input.mousePosition.x <= 0 )|| (Input.GetKey("left") && Input.GetKey("up")))
        {
            transform.position += panSpeed * (Vector3.left + Vector3.up);
        }
        else if ((Input.mousePosition.y <= 0 && Input.mousePosition.x >= Screen.width) || (Input.GetKey("right") && Input.GetKey("down")))
        {
            transform.position += panSpeed * (Vector3.right + Vector3.down);
        }
        else if ((Input.mousePosition.y <= 0 && Input.mousePosition.x <= 0) || ((Input.GetKey("left") && Input.GetKey("down") )))
        {
            transform.position += panSpeed * (Vector3.left + Vector3.down);
        }
        else if (Input.mousePosition.x >= Screen.width || Input.GetKey("right"))
        {
            transform.position += panSpeed * Vector3.right;
        } 
        else if (Input.mousePosition.x <= 0 || Input.GetKey("left"))
        {
            transform.position += panSpeed * Vector3.left;
        }
        else if (Input.mousePosition.y <= 0 || Input.GetKey("down"))
        {
            transform.position += panSpeed * Vector3.down;
        }
        else if (Input.mousePosition.y >= Screen.height || Input.GetKey("up"))
        {
            transform.position += panSpeed * Vector3.up;
        }
    }
    void OnGUI()
    {
        if (_isDraggingMouseBox)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(_dragStartPosition, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.5f, 1f, 0.4f, 0.2f));
            Utils.DrawScreenRectBorder(rect, 1, new Color(0.5f, 1f, 0.4f));
        }
    }
}
