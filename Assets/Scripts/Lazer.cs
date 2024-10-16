using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private LayerMask reflective;
    [SerializeField] private float defaultDistanceRay = 100;
    [SerializeField] private int maxReflections = 5;
    [SerializeField] private float reflectionOffset = 0.25f;
    public Transform laserFirePoint;
    public LineRenderer mLineRenderer;

    private Transform mTransform;

    private void Awake()
    {
        mTransform = GetComponent<Transform>();
    }

    void Update()
    {
        ShootLazer();
    }

    void ShootLazer()
    {
        Vector2 laserDirection = laserFirePoint.transform.right; // Initial direction of the laser
        Vector2 currentPosition = laserFirePoint.position; // Start point of the laser

        // Array to store the points of the laser to draw them later
        List<Vector2> laserPoints = new List<Vector2>();
        laserPoints.Add(currentPosition);

        int reflectionsRemaining = maxReflections;

        while (reflectionsRemaining > 0)
        {
            // Raycast in current direction
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, laserDirection);

            if (hit)
            {
                laserPoints.Add(hit.point); // Add the hit point to the laser

                // Check if hit object detects light
                if (hit.collider.GetComponent<DetectsLight>() != null)
                {
                    hit.collider.GetComponent<DetectsLight>().isLit = true; // Marks the object as lit
                }

                // Check if the hit object is reflective using some weird bit-shit that I dont understand but it works
                if (((1 << hit.collider.gameObject.layer) & reflective) != 0)
                {
                    // Calculate the reflection direction using the hit normal
                    laserDirection = Vector2.Reflect(laserDirection, hit.normal);
                    currentPosition = hit.point + laserDirection * reflectionOffset; // Start the new laser from the hit point
                    reflectionsRemaining--; // Reduce the number of remaining reflections
                }
                else
                {
                    // If the object is not reflective, stop the laser here
                    break;
                }
            }
            else
            {
                // If no hit, set the laser to the default distance
                laserPoints.Add(currentPosition + laserDirection * defaultDistanceRay);
                break;
            }
        }

        // Draw the laser using LineRenderer
        Draw2DRay(laserPoints);
    }

    void Draw2DRay(List<Vector2> laserPoints)
    {
        // Set the number of positions in the LineRenderer based on the laser points
        mLineRenderer.positionCount = laserPoints.Count;

        for (int i = 0; i < laserPoints.Count; i++)
        {
            mLineRenderer.SetPosition(i, laserPoints[i]);
        }
    }
}
