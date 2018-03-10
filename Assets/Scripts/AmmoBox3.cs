using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox3 : MonoBehaviour {
	public int quantity = 4;

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag != "Player") {
			return;
		}
		Debug.Log (col.gameObject.name);
		col.gameObject.BroadcastMessage("Reload3", quantity);

		Object.Destroy (gameObject);
	}
}
