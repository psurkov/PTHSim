using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour {

    public Vector3 target;
    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target); // приказываем идти на target
	}
}
