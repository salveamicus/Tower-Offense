using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCamera : MonoBehaviour
{
    public float panSpeed = 8f;
    public float scrollSpeed = 20f;
    public float borderRadius = 10f;
    public float speedMult = 1f;

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

        if ((Input.mouseScrollDelta.y > 0) && (Camera.main.orthographicSize > 3))
        {
            Camera.main.orthographicSize -= scrollSpeed * Time.deltaTime;
            Camera.main.nearClipPlane -= scrollSpeed * Time.deltaTime;
            borderRadius += scrollSpeed * Time.deltaTime;
        }
        else if ((Input.mouseScrollDelta.y < 0) && (Camera.main.orthographicSize < 10))
        {
            Camera.main.orthographicSize += scrollSpeed * Time.deltaTime;
            Camera.main.farClipPlane += scrollSpeed * Time.deltaTime;
            borderRadius -= scrollSpeed * Time.deltaTime;
        }

        if ((Input.mousePosition.y >= Screen.height && Input.mousePosition.x >= Screen.width) || ((Input.GetKey("right") || Input.GetKey("d")) && (Input.GetKey("up") || Input.GetKey("w"))))
        {
            if ((transform.position.x < borderRadius) && (transform.position.y < borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * (Vector3.right + Vector3.up);
            }
            else if ((transform.position.x < borderRadius) && !(transform.position.y < borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.right;
            }
            else if (!(transform.position.x < borderRadius) && (transform.position.y < borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.up;
            }
        }
        else if ((Input.mousePosition.y >= Screen.height && Input.mousePosition.x <= 0 )|| ((Input.GetKey("left") || Input.GetKey("a")) && (Input.GetKey("up") || Input.GetKey("w"))))
        {
            if ((transform.position.x > -borderRadius) && (transform.position.y < borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * (Vector3.left + Vector3.up);
            }
            else if ((transform.position.x > -borderRadius) && !(transform.position.y < borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.left;
            }
            else if (!(transform.position.x > -borderRadius) && (transform.position.y < borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.up;
            }
        }
        else if ((Input.mousePosition.y <= 0 && Input.mousePosition.x >= Screen.width) || ((Input.GetKey("right") || Input.GetKey("d")) && (Input.GetKey("down") || Input.GetKey("s"))))
        {
            if ((transform.position.x < borderRadius) && (transform.position.y > -borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * (Vector3.right + Vector3.down);
            }
            else if ((transform.position.x < borderRadius) && !(transform.position.y > -borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.right;
            }
            else if (!(transform.position.x < borderRadius) && (transform.position.y > -borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.down;
            }
        }
        else if ((Input.mousePosition.y <= 0 && Input.mousePosition.x <= 0) || (((Input.GetKey("left") || Input.GetKey("a")) && (Input.GetKey("down") || Input.GetKey("s")) )))
        {
            if ((transform.position.x > -borderRadius) && (transform.position.y > -borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * (Vector3.left + Vector3.down);
            }
            else if ((transform.position.x > -borderRadius) && !(transform.position.y > -borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.left;
            }
            else if (!(transform.position.x > -borderRadius) && (transform.position.y > -borderRadius))
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.down;
            }
        }
        else if (Input.mousePosition.x >= Screen.width || (Input.GetKey("right") || Input.GetKey("d")))
        {
            if (transform.position.x < borderRadius)
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.right;
            }
        } 
        else if (Input.mousePosition.x <= 0 || (Input.GetKey("left") || Input.GetKey("a")))
        {
            if (transform.position.x > -borderRadius)
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.left;
            }
        }
        else if (Input.mousePosition.y <= 0 || (Input.GetKey("down") || Input.GetKey("s")))
        {
            if (transform.position.y > -borderRadius)
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.down;
            }
        }
        else if (Input.mousePosition.y >= Screen.height || (Input.GetKey("up") || Input.GetKey("w")))
        {
            if (transform.position.y < borderRadius)
            {
                transform.position += panSpeed * Time.deltaTime * Vector3.up;
            }
        }
    }
    void OnGUI()
    {
        if (_isDraggingMouseBox && !gameStatistics.purchasingUnit)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(_dragStartPosition, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.5f, 1f, 0.4f, 0.2f));
            Utils.DrawScreenRectBorder(rect, 1, new Color(0.5f, 1f, 0.4f));
        }
    }
}
