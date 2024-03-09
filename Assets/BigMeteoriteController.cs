using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteoriteController : MonoBehaviour
{
    bool isWrappingX = false;
    bool isWrappingY = false;
    private float floatSpeed = 2f;
    Vector2 randomDirection;
    // Start is called before the first frame update
    void Start()
    {
        randomDirection = Random.insideUnitCircle.normalized;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(randomDirection * floatSpeed * Time.deltaTime);
        ScreenWrap();
    }

    void ScreenWrap()
    {
        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;
        float wrapOffset = 0.02f; // Adjust the offset value as needed

        // Check if the object has wrapped around the screen in the x-axis
        if (!isWrappingX && (viewportPosition.x > 1 + wrapOffset || viewportPosition.x < -wrapOffset))
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }
        // Reset the isWrappingX flag if the object is within the viewport
        else if (viewportPosition.x >= 0 - wrapOffset && viewportPosition.x <= 1 + wrapOffset)
        {
            isWrappingX = false;
        }

        // Check if the object has wrapped around the screen in the y-axis
        if (!isWrappingY && (viewportPosition.y > 1 + wrapOffset || viewportPosition.y < -wrapOffset))
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }
        // Reset the isWrappingY flag if the object is within the viewport
        else if (viewportPosition.y >= 0 - wrapOffset && viewportPosition.y <= 1 + wrapOffset)
        {
            isWrappingY = false;
        }

        transform.position = newPosition;
    }


}
