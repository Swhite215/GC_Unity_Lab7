using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneMenuLoad : MonoBehaviour {

	public GameObject menuAlias;
	public GameObject menuFollower;

	void OnTriggerEnter(Collider collider) {
		Debug.Log(collider);
		if (collider.tag == "PlayerTarget") {
			menuAlias.SetActive (true);
			menuFollower.SetActive (true);
		}
	}
}
