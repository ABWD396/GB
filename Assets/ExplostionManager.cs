using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplostionManager : MonoBehaviour
{

    private List<Rigidbody> rigidbodies;
    private Transform mainTransform;
    public float explodeForce = 100;

    private void Awake()
    {
        mainTransform = GetComponent<Transform>();
        rigidbodies = new List<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        rigidbodies.Add(other.gameObject.GetComponent<Rigidbody>());
    }

    private void OnTriggerExit(Collider other)
    {
        rigidbodies.Remove(other.gameObject.GetComponent<Rigidbody>());
    }


    public void Explode()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            Vector3 angle = rigidbody.transform.position - mainTransform.position;
            angle.Normalize();
            float distance = Vector3.Distance(mainTransform.position, rigidbody.transform.position);

            angle = angle * (explodeForce / distance);


            rigidbody.AddForce(angle, ForceMode.VelocityChange);
            rigidbody.AddTorque(angle, ForceMode.VelocityChange);
        }
    }


    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
