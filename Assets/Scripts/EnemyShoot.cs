using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
	public GameObject projectile;
	public GameObject shotPoint;
	public float shotForce = 8.0f;
	public float shotTTL = 5.0f;
	public float rechargeTime = 2.2f;
	public float startDelay = 5.0f;

	public AudioClip launchNoise;

	private float lastShotTime;

	void Start() {
		lastShotTime = Time.time + startDelay;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > lastShotTime + rechargeTime) {
			Shoot ();
		}
	}

	void Shoot () {
		lastShotTime = Time.time;

		if (launchNoise != null) {
			AudioSource.PlayClipAtPoint (launchNoise, shotPoint.transform.position, 1.0f);
		}

		GameObject newshot = Object.Instantiate (projectile, 
			shotPoint.transform.position, 
			this.transform.rotation);

		newshot.GetComponent<Rigidbody>().AddForce(transform.forward * shotForce, ForceMode.Impulse);

		Object.Destroy (newshot, shotTTL);
	}
}