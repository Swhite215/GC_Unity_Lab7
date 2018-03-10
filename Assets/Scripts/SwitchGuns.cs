using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGuns : MonoBehaviour {
	public GameObject [] guns;
	public int activeGun = 0;

	void Start () {
		EnableGun ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftAlt) == true) {
			activeGun++;
			if (activeGun == guns.Length) {
				activeGun = 0;
			}
		
			EnableGun ();
		}
	}

	void EnableGun () {
		foreach (GameObject gun in guns) {
			gun.SetActive (false);
		}

		guns [activeGun].SetActive (true);
	}
}
