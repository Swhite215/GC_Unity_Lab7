using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private Rigidbody rb;
	public float attackDamage = 10;

	//Speed
	public float speed;

	//Target (Set by AI Script)
	public Transform target;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
		Destroy(gameObject, 3.0f);
	}

	void FixedUpdate() 
	{
		if (target != null) {
			Vector3 dir = target.position - transform.position;
			rb.velocity = dir.normalized * speed;
		} else {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
