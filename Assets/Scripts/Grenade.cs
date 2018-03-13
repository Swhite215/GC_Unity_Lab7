using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

	public float delay = 3f;
	public float blastRadius = 5f;
	public float force = 100f;

	public GameObject explosionEffect;

	public float countdown;
	bool hasExploded = false;


	// Use this for initialization
	void Start () {
		countdown = delay;
	}

	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if (countdown <= 0f && !hasExploded) {
			Explode ();
			hasExploded = true;
		}
	}

	void Explode() {
		//show expl effect
		Instantiate(explosionEffect, transform.position, transform.rotation);
		//find nearby obj
		Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
		foreach(Collider nearbyObject in colliders){
			//force
			Rigidbody rb = 	nearbyObject.GetComponent<Rigidbody>();
			if (rb != null) {
				rb.AddExplosionForce (force, transform.position, blastRadius);
			}
			//destroy
		}
		Destroy(gameObject);
	}
}