using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//https://www.youtube.com/watch?v=cLzG1HDcM4s&t=321s Reference materiale for making interactable Game Objects

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode InteractKey;
    public UnityEvent InteractAction;

    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed;
    private int direction = 1;

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKey(InteractKey))
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player is in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player is not in range");
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
}
