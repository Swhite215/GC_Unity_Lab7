namespace VRTK.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	[System.Serializable]
	public class Shoot2 : VRTK_InteractableObject
	{
		public GameObject shotPoint;
		public float shotForce = 2.0f;

		public GameObject shotPuff;
		public GameObject muzzleFlash;

		public float rechargeTime = 0.1f;
		public float range = 500f;

		public AudioClip machineGunSound;
		public AudioClip ricochetSound;
		public Sprite Icon;
		public bool grabbed = false;

		private float lastShotTime;
		private VRTK_ControllerReference controllerReference;

		public override void Grabbed(VRTK_InteractGrab grabbingObject)
		{
			base.Grabbed(grabbingObject);
			controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
			if (!InventoryManager.instance.IsWeaponAlreadyInInventory(gameObject))
			{
				InventoryManager.instance.AddWeapon(gameObject);
				if (Icon != null)
				{
					InventoryManager.instance.ChangeInventoryIcon(Icon);
				}
			}
			
		}

		public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
		{
			gameObject.SetActive(false);
			//base.Ungrabbed(previousGrabbingObject);
			controllerReference = null;
			gameObject.GetComponent<BoxCollider>().isTrigger = false;
		}
		void Start()
		{
			lastShotTime = Time.time - rechargeTime;
			ChangeParent();
		}
		private void ChangeParent()
		{
				transform.parent = GameObject.FindGameObjectWithTag("RightController").transform;
		}
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			if (Time.time > lastShotTime + rechargeTime)
			{
				Shoot();
			}
		}
		// Update is called once per frame

		void Shoot()
		{
			GameManager.shots++;
			Debug.Log("Shoot!");
			lastShotTime = Time.time;
			RaycastHit info;

			if (muzzleFlash != null)
			{
				GameObject flash = Object.Instantiate(muzzleFlash, shotPoint.transform.position, Quaternion.identity);
				Destroy(flash, .05f);
			}
			if (machineGunSound != null)
			{
				AudioSource.PlayClipAtPoint(machineGunSound, shotPoint.transform.position, 1.0f);
			}

			//if(shotPuff != null && Physics.Raycast(this.transform.position, this.transform.forward * range,out info, range))
			//the particular gun model we're using, down (up * -1) is the barrel direction
			if (shotPuff != null && Physics.Raycast(this.transform.position, this.transform.up * -1 * range, out info, range))
			{
				if (info.collider.tag == "Terrain" || info.collider.tag == "Target" || info.collider.tag == "Enemy")
				{
					GameManager.hits++;
					Vector3 hitSpot = info.point;
					GameObject puff = Object.Instantiate(shotPuff, hitSpot, Quaternion.identity);
					Destroy(puff, 1f);

					if (ricochetSound != null)
					{
						AudioSource.PlayClipAtPoint(ricochetSound, hitSpot, 1.0f);
					}

					Rigidbody rb = info.collider.gameObject.GetComponent<Rigidbody>();

					if (rb != null)
					{
						rb.AddExplosionForce(shotForce, hitSpot, 1.0f, 0, ForceMode.Impulse);
					}
				}
				if (info.collider.tag == "Enemy")
				{
					EnemyHP hp = info.collider.gameObject.GetComponent<EnemyHP>();
					hp.ChangeHP(-1 * shotForce);
				}
			}
		}
	}

}




