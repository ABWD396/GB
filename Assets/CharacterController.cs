using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{

    private NavMeshAgent agent;

    [SerializeField]
    private GameObject target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.destination = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
