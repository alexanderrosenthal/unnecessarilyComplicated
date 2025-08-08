using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rig;
    public float moveSpeed = 10f;
    bool useTorque = true;

    float inputX;
    float inputZ;

    void Start()
    {
        // Interpolation aktivieren
        if (rig != null)
        {
            rig.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    void Update()
    {
        // Eingaben nur hier abfragen
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Physik nur hier
        if (useTorque)
        {
            Vector3 torque = new Vector3(inputX, 0f, inputZ) * moveSpeed;
            rig.AddTorque(torque, ForceMode.Force);
        }
    }
}