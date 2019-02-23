using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour {

    public Vector3 target;
    private NavMeshAgent agent;
    private bool isIdle = true;
    private float idle_time = 0f;
    private float timeBeforePassiveMove = 3f;
    private float needLen2Target = 0.6f;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (isIdle)
            idle_time += Time.deltaTime;
        agent.SetDestination(target); // приказываем идти на target
        if ((target - transform.position).magnitude < needLen2Target)
        {
            isIdle = true;
            target = transform.position;
        }
        else
        {
            isIdle = false;
            idle_time = 0;
        }

        if (idle_time > timeBeforePassiveMove)
        {
            GameObject[] passive_targets = GameObject.FindGameObjectsWithTag("Passive target");
            target = passive_targets[Random.Range(0, passive_targets.Length - 1)].transform.position;
        }
	}
}
