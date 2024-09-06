using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float speed = 1.0f;
    public LevelController levelController;


    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(rb.velocity.normalized * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Finish"))
        {
            levelController.FinishLevel();
        }
        else if (other.gameObject.tag.Equals("Respawn")) {
            levelController.FailLevel();
        }
    }
}
