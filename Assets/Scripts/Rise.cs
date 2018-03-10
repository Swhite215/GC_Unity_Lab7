using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rise : MonoBehaviour {

	public Animator animator;

	void OnTriggerEnter(Collider collider) {
        Debug.Log(collider);
		if (collider.tag == "PlayerTarget") {
			animator.SetTrigger ("Rise");
		}
	}


}
