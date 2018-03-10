using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot2 : MonoBehaviour {
	public GameObject shotPoint;
	public float shotForce = 2.0f;

	public GameObject shotPuff;
	public GameObject muzzleFlash;

	public float rechargeTime = 0.1f;
	public float range = 500f;

	public AudioClip machineGunSound;
	public AudioClip ricochetSound;

	private float lastShotTime;

	void Start() {
		lastShotTime = Time.time - rechargeTime;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Fire1") > 0 && Time.time > lastShotTime + rechargeTime) {
			Shoot ();
		}
	}

	void Shoot () {
		GameManager.shots++;
		Debug.Log ("Shoot!");
		lastShotTime = Time.time;
		RaycastHit info;

		if (muzzleFlash != null) {
			GameObject flash = Object.Instantiate(muzzleFlash, shotPoint.transform.position, Quaternion.identity); 
			Destroy(flash, .05f);
		}
		if (machineGunSound != null) {
			AudioSource.PlayClipAtPoint (machineGunSound, shotPoint.transform.position, 1.0f);
		}

		//if(shotPuff != null && Physics.Raycast(this.transform.position, this.transform.forward * range,out info, range))
		//the particular gun model we're using, down (up * -1) is the barrel direction
		if(shotPuff != null && Physics.Raycast(this.transform.position, this.transform.up * -1 * range,out info, range))
		{
			if(info.collider.tag == "Terrain" || info.collider.tag == "Target" || info.collider.tag == "Enemy")
			{ 
				GameManager.hits++;
				Vector3 hitSpot = info.point;   
				GameObject puff = Object.Instantiate(shotPuff, hitSpot, Quaternion.identity);
				Destroy(puff, 1f);

				if (ricochetSound != null) {
					AudioSource.PlayClipAtPoint (ricochetSound, hitSpot, 1.0f);
				}

				Rigidbody rb = info.collider.gameObject.GetComponent<Rigidbody> ();
					
				if (rb != null) {
					rb.AddExplosionForce (shotForce, hitSpot, 1.0f, 0, ForceMode.Impulse);
				}
			}
			if (info.collider.tag == "Enemy") {
				EnemyHP hp = info.collider.gameObject.GetComponent<EnemyHP> ();
				hp.ChangeHP (-1 * shotForce);
			}
		}
	}
}




