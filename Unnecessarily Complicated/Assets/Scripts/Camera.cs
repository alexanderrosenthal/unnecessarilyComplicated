using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Kugel

    [Header("Follow Settings")]
    public float smoothSpeed = 0.125f;       
    public Vector3 offset = new Vector3(0f, 5f, -10f); 

    void LateUpdate()
    {
        if (target == null) return;

        // Position = Kugelposition + statischer Offset (keine Rotation der Kugel)
        Vector3 desiredPosition = target.position + offset;

        // Weiche Bewegung
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Kamera schaut zur Kugel, ohne Kippen
        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0f; 
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
}