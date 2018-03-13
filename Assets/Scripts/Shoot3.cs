namespace VRTK.Examples
{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Shoot3 : Shoot1 {
		public int ammo = 6;
		public Text ammoDisplay;

		void Start()
		{
			lastShotTime = Time.time - rechargeTime;
		}
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			if (Time.time > lastShotTime + rechargeTime)
				if (ammo > 0)
				{
					Shoot();
					ammo--;
				}
		}
		void Update () {
			if (ammoDisplay != null) {
				if (ammo < 10) {
					ammoDisplay.text = "-" + ammo + "-";
				} else {
					ammoDisplay.text = "" + ammo;
				}
			}
		}

		public void Reload3 (int q) {
			ammo += q;
		}
	}

}