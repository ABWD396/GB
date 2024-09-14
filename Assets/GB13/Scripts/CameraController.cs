using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Transform trans;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float inertiaMovement = 1;

    bool followPlayer = true;


    private void Start()
    {
        trans = GetComponent<Transform>();
        offset = trans.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            trans.position = player.position + offset;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Finish"))
        {
            followPlayer = false;
        }
    }
}
