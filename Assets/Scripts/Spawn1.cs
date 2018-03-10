using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn1 : MonoBehaviour {
	public GameObject enemy;
	public GameObject spawnPoint;
	public float rechargeTime = 2.2f;
	public float startDelay = 5.0f;

	public int maxDrones = 3;
	public int radius = 5;

	private float lastSpawnTime;

	// Update is called once per frame
	void Update () {
		if (Time.time > lastSpawnTime + rechargeTime && Drone.droneCount < maxDrones) {
			Spawn ();
		}
	}

	void Spawn () {
		lastSpawnTime = Time.time;

		GameObject newEnemy = Object.Instantiate (enemy, 
			spawnPoint.transform.position + Random.onUnitSphere * radius, 
			this.transform.rotation);
		newEnemy.transform.Rotate(new Vector3 (0, Random.Range (0, 360), 0));
	}
}