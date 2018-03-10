using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
	public GameObject prefabToGenerate;
	public float rechargeTime = 3.0f;

	private float lastGeneratedTime;

	void Start () {
		lastGeneratedTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > lastGeneratedTime + rechargeTime) {
			Object.Instantiate (prefabToGenerate, transform);
			lastGeneratedTime = Time.time;
		}
	}
}