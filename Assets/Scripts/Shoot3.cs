namespace VRTK.Examples
{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	[System.Serializable]
	public class Shoot3 : VRTK_InteractableObject
	{
		public GameObject projectile;
		public GameObject shotPoint;
		public float shotForce = 8.0f;
		public float shotTTL = 5.0f;
		public float rechargeTime = 2.2f;
		public Sprite icon;
		public int ammo = 6;
		public Text ammoDisplay;


		public AudioClip launchNoise;

		protected float lastShotTime;
		private VRTK_ControllerReference controllerReference;

		public override void Grabbed(VRTK_InteractGrab grabbingObject)
		{
			base.Grabbed(grabbingObject);
			controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
			if (!InventoryManager.instance.IsWeaponAlreadyInInventory(gameObject))
			{
				InventoryManager.instance.AddWeapon(gameObject);
				if (icon != null)
				{
					InventoryManager.instance.ChangeIcons();
				}
			}


		}
		public void Start()
		{
			lastShotTime = Time.time - rechargeTime;
			ChangeParent();
		}

		public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
		{
			gameObject.SetActive(false);
			//base.Ungrabbed(previousGrabbingObject);
			controllerReference = null;
			gameObject.GetComponent<BoxCollider>().isTrigger = false;
		}

		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			//Debug.Log("trigger pull");
			base.StartUsing(usingObject);
			if (Time.time > lastShotTime + rechargeTime)
			{
				Shoot();
			}
		}
		private void ChangeParent()
		{
			transform.parent = GameObject.FindGameObjectWithTag("RightController").transform;
		}
		protected void Shoot()
		{
			GameManager.shots++;

			lastShotTime = Time.time;

			if (launchNoise != null)
			{
				AudioSource.PlayClipAtPoint(launchNoise, shotPoint.transform.position, 1.0f);
			}
			VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, 1.0f, 0.2f, 0.01f);

			GameObject newshot = Object.Instantiate(projectile,
				shotPoint.transform.position,
				this.transform.rotation);

			newshot.GetComponent<Rigidbody>().AddForce(transform.up * shotForce, ForceMode.Impulse);

			Object.Destroy(newshot, shotTTL);
		}
	}
}