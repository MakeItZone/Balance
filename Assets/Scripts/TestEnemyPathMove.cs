using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyPathMove : MonoBehaviour {

	private Transform goal;
	private NavMeshAgent agent;

	// Start is called before the first frame update
	void Start() {
		goal = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update() {
		agent.SetDestination(goal.position);
	}
}
