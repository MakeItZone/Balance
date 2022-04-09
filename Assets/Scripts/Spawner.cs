using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject spawn;
	public int amount;
	public float delay;

	private int getAmount;
	private int spawned;
	private float timer;

	// Start is called before the first frame update
	void Start() {
		ResetRound();
	}

	private void ResetRound() {
		getAmount = amount;
	}

	// Update is called once per frame
	void Update() {
		timer += Time.deltaTime;
		if(timer > delay) {
			if(spawned < getAmount) {
				timer = 0;
				spawned++;
				GameObject instance = Instantiate(spawn, transform);
				instance.transform.parent = null;
			}
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		if(spawn != null) {
			Gizmos.DrawWireMesh(spawn.GetComponent<MeshFilter>().sharedMesh,transform.position, spawn.transform.rotation, Vector3.one);
		}
	}
}
