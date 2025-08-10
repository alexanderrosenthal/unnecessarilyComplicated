using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rig;
    public float turnTorque = 5f;   // Drehmoment für links/rechts
    public float moveForce = 10f;   // Vortriebskraft vor/zurück
    private float inputX;
    private float inputZ;

    //------------------- BASICS ---------------------------------------------------------
    void Start()
    {
        if (rig != null)
        {
            rig.interpolation = RigidbodyInterpolation.Interpolate;
            rig.maxAngularVelocity = 100f; // verhindert Begrenzung bei hohem Drehmoment
        }
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal"); // A/D -> seitliches Rollen
        inputZ = Input.GetAxisRaw("Vertical");   // W/S -> vor/zurück Rollen
    }

    void FixedUpdate()
    {
        // 1. Links/Rechts rollen
        if (Mathf.Abs(inputX) > 0.01f)
        {
            Vector3 torqueDir = -transform.forward * inputX * turnTorque;
            rig.AddTorque(torqueDir, ForceMode.VelocityChange);
        }

        // 2. Vorwärts/Rückwärts rollen
        if (Mathf.Abs(inputZ) > 0.01f)
        {
            Vector3 torqueDir = transform.right * inputZ * moveForce;
            rig.AddTorque(torqueDir, ForceMode.VelocityChange);
        }
    }
}