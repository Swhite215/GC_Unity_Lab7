using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAi : MonoBehaviour {

	public GameObject shot;
	public GameObject shotSpawn;
	private NavMeshAgent agent;
	private Transform PlayerTransform;

	public float rechargeTime = 3.0f;
	private float lastGeneratedTime;

	// Use this for initialization
	void Start () {
		GameObject PlayerObject = GameObject.FindGameObjectWithTag ("PlayerTarget");
		PlayerTransform = PlayerObject.GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(PlayerTransform.position);

		if (shot != null) {
			if (Time.time > lastGeneratedTime + rechargeTime) {
				fireShot (PlayerTransform.gameObject);
				lastGeneratedTime = Time.time;
			}
		}
	}

	void fireShot(GameObject enemy) {
		GameObject arrow = (GameObject)Instantiate (shot, shotSpawn.transform.position, Quaternion.identity);
		arrow.GetComponent<Projectile> ().target = enemy.transform;
	}

}
