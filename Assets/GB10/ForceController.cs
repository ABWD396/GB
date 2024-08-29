using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    Rigidbody[] wheels;

    public float torqueForce = 2f;
    public float maxAngularVelocity = 15f;

    // Start is called before the first frame update
    void Start()
    {
        wheels = GetComponentsInChildren<Rigidbody>();       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Rigidbody wheel in wheels) {
            wheel.maxAngularVelocity = maxAngularVelocity;

            wheel.AddRelativeTorque(0, -1 * torqueForce, 0, ForceMode.Force);
        }
    }
}
