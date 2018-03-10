using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {
	public static int droneCount = 0;

	public float patrolSpeed = 10;
	public float huntSpeed = 20;

	public float shotStrength = 10;
	public float minDistance = 10;
	public float alertDistance = 40;

	public GameObject shotPoint;
	public GameObject shot;

	public GameObject point1;
	public GameObject point2;

	public enum DroneStatus { going, returning, hunting };
	public DroneStatus status;

	private GameObject target;
	private float closeEnough = .25f;

	void Start() {
		droneCount++;

		status = DroneStatus.going;
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (status == DroneStatus.going) {
			if (!CheckForTarget ()) {
				MoveTwd (point1, patrolSpeed);

				if (CheckDistance (point1, closeEnough)) {
					status = DroneStatus.returning;
				}
			}
		} else if (status == DroneStatus.returning) {
			if (!CheckForTarget ()) {
				MoveTwd (point2, patrolSpeed);

				if (CheckDistance (point2, closeEnough)) {
					status = DroneStatus.going;
				}
			}
		} 

		//separate if in case player is picked up this frame
		if (status == DroneStatus.hunting) {
			if (!CheckDistance (target, minDistance)) {
				MoveTwd (target, huntSpeed);
			} else {
				this.transform.LookAt (target.transform);
				if (transform.position.y > target.transform.position.y) {
					transform.position += new Vector3 (0, -1 * Time.deltaTime, 0);
				}
			}
		}

	}

	bool CheckDistance (GameObject obj, float dist) {
		if (Vector3.Distance (this.transform.position, obj.transform.position) <= dist) {
			return true;
		} else {
			return false;
		}
	}

	bool CheckForTarget () {
		if (Vector3.Distance (this.transform.position, target.transform.position) <= alertDistance) {
			status = DroneStatus.hunting;
			return true;
		}
		return false;
	}

	//not to be confused with Vector3.MoveTowards()
	void MoveTwd (GameObject obj, float speed) {
		this.transform.LookAt (obj.transform);
		float step = speed * Time.deltaTime;
		transform.Translate (Vector3.forward * step);
		//transform.position = Vector3.MoveTowards (transform.position, obj.transform.position, step);
	}

	//need to destroy the parent PatrolDrone
	void OnDestroy() {
		droneCount--;
		Object.Destroy (transform.parent.gameObject);
	}
}
