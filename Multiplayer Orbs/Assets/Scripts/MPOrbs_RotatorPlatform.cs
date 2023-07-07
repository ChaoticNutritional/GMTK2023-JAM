using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPOrbs_RotatorPlatform : MonoBehaviour
{

    [Tooltip("The torque added to the platform. + rotates counterclockwise, - rotates clockwise.")]
    public float rotationSpeed;
    [Tooltip("The maximum AND starting velocity, in radians per second.")]
    public float maxVelocity = 1;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if (rotationSpeed < 0)
            maxVelocity = -Mathf.Abs(maxVelocity);
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Vector3.forward * maxVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        // If we're not at max velocity (or if the velocity has somehow gone negative), apply torque.
        if((Mathf.Abs(rb.angularVelocity.z) <= Mathf.Abs(maxVelocity)) || rb.angularVelocity.z / maxVelocity < 0)
            rb.AddTorque(0, 0, rotationSpeed * rb.mass);
    }
}
