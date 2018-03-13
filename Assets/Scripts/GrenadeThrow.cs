using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]

public class GrenadeThrow : MonoBehaviour
{

	public float throwForce = 1;
	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;
	public GameObject controllerModel;
	//public GameObject grenadePrefab;

	public float delay = 3f;
	public float blastRadius = 5f;
	public float force = 100f;

	public GameObject explosionEffect;

	public bool objectHeld = false;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update()
	{
		device = SteamVR_Controller.Input((int)trackedObj.index);
	}

	void OnTriggerStay(Collider col)
	{
		if (col.CompareTag("Grenade"))
		{
			if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
			{
				col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				col.gameObject.transform.SetParent(gameObject.transform);
				col.gameObject.transform.position = gameObject.transform.position;
				col.gameObject.transform.localPosition += new Vector3(0f, -.05f, -0.07f);
				objectHeld = true;
				controllerModel.SetActive(false);
			}
			//if the object has been picked up
			if ((objectHeld == true) && (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)))
			{
				col.gameObject.transform.SetParent(null);
				col.attachedRigidbody.isKinematic = false;

				objectHeld = false;

				ThrowObject(col.attachedRigidbody);
				//Destroy(col.gameObject, 5);
				Invoke("Explode", delay);
				controllerModel.SetActive(true);


			}
		}
	}

	void ThrowObject(Rigidbody rigidBody)
	{
		rigidBody.velocity = Quaternion.Euler(Vector3.forward) * device.velocity * throwForce;
		rigidBody.angularVelocity = Quaternion.Euler(Vector3.forward) * device.angularVelocity;
	}

	void Explode() {
		//show expl effect
		Instantiate(explosionEffect, transform.position, transform.rotation);
		//find nearby obj
		Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
		foreach(Collider nearbyObject in colliders){
			//force
			Rigidbody rb = 	nearbyObject.GetComponent<Rigidbody>();
			if (rb != null) {
				rb.AddExplosionForce (force, transform.position, blastRadius);
			}
			//destroy
		}
		Destroy(gameObject);
	}
}