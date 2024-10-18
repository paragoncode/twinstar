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

    public Animator anim;
    // Use this for initialization
    void Start()
    {
     anim = gameObject.GetComponent<Animator>();
    }
    
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

                anim.Play("Flip Green");

                if (distance <= 0.2f)
                {
                    direction *= -1;
                }
            }
            else
            {
                anim.Play("Flip Red");
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player is in range");
            //collision.transform.SetParent(platform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player is not in range");
            //collision.transform.SetParent(null);
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
