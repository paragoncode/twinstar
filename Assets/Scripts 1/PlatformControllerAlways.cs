using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformControllerAlways : MonoBehaviour
{
    public Transform platformAlways;
    public Transform startAlwaysPoint;
    public Transform endAlwaysPoint;
    public float speed;
    private int direction = 1;


    private void Update()
    {
        Vector2 target = currentMovementTarget(); // Platform should move only when isMoving is true
        
        platformAlways.position = Vector2.Lerp(platformAlways.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platformAlways.position).magnitude;

        if (distance <= 0.2f)
        
        {
            direction *= -1;
        }
    }

    Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return endAlwaysPoint.position; // Move to endPoint
        }
        else
        {
            return startAlwaysPoint.position; // Move to startPoint
        }
    }

    private void OnDrawGizmos()
    {
        if (platformAlways != null && startAlwaysPoint != null && endAlwaysPoint != null)
        {
            Gizmos.DrawLine(platformAlways.position, startAlwaysPoint.position);
            Gizmos.DrawLine(platformAlways.position, endAlwaysPoint.position);
        }
    }
}
