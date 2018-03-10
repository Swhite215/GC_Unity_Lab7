namespace VRTK.Examples
{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Shoot3 : Shoot1 {
		public int ammo = 6;
		public Text ammoDisplay;

		void Update () {
			if (ammoDisplay != null) {
				if (ammo < 10) {
					ammoDisplay.text = "-" + ammo + "-";
				} else {
					ammoDisplay.text = "" + ammo;
				}
			}
			if (Input.GetAxis("Fire1") > 0 && Time.time > lastShotTime + rechargeTime) {
				if (ammo > 0) {
					Shoot ();
					ammo--;
				}
			}
		}

		public void Reload3 (int q) {
			ammo += q;
		}
	}

}