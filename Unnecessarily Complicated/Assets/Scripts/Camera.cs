using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    // Offset für schrägen Blick von oben rechts
    public Vector3 offset = new Vector3(3f, 7f, -10f);

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0f; // keine Kippung der Kamera
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
}