using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour {
	public float hp;

	public void ChangeHP (float n) {
		//Debug.Log (gameObject.name + " hit for " + n);
		hp += n;

		if (hp <= 0) {
			Object.Destroy (gameObject);
		}
	}
}
