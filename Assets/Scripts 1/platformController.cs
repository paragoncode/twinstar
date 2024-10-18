using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed;
    private int direction = 1;
    public bool isMoving = false;

    public void Update()
    {
        if (isMoving) // Platform should move only when isMoving is true
        {
            Vector2 target = currentMovementTarget();
            platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

            float distance = (target - (Vector2)platform.position).magnitude;

            if (distance <= 0.2f)
            {
                direction *= -1;
            }
        }
    }

    Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return endPoint.position; // Move to endPoint
        }
        else
        {
            return startPoint.position; // Move to startPoint
        }
    }

    private void OnDrawGizmos()
    {
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.position, startPoint.position);
            Gizmos.DrawLine(platform.position, endPoint.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered platform");
            // Set the player as a child of the platform
            //collision.transform.SetParent(platform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Remove the player from the platform when they exit
            //collision.transform.SetParent(null);
        }
    }
}
