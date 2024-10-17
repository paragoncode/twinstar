using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public platformController platformControllerScript; // Reference to the platform controller

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box")) // Assuming the player has a "Player" tag
        {
            platformControllerScript.isMoving = true; // Start the platform's movement
            Debug.Log("Platform started moving");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box"))
        {
            platformControllerScript.isMoving = false; // Stop the platform's movement
            Debug.Log("Platform stopped moving");
        }
    }
}


