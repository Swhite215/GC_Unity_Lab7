using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static int shots = 0;
	public static int hits = 0;
	public AudioClip music;

	void Start()
	{
		if (music != null && MenuManager.playMusic)
		{
			StartCoroutine("AttachToListener"); 
		}
	}

	// Update is called once per frame
	void Update () {
		if (shots > 0) {
			float hitPct = hits / (float)shots * 100;
			hitPct = Mathf.RoundToInt (hitPct);
		} 
	}

	IEnumerator AttachToListener ()
	{
		bool setup = false;
		while (!setup)
		{
			AudioListener listener = GameObject.FindObjectOfType<AudioListener>();

			//if the audiolistener hasn't been set up yet,
			//wait for 100 milliseconds and try again
			if (listener == null)
			{
				Debug.Log("Not attached");
				yield return new WaitForSeconds(.1f);
			}
			else
			{
				GameObject audioPlayer = new GameObject();
				audioPlayer.transform.SetParent(listener.transform);
				audioPlayer.AddComponent(typeof(AudioSource));

				GameObject playerCollider = new GameObject ();
				playerCollider.transform.SetParent (listener.transform);
				playerCollider.AddComponent (typeof(BoxCollider));
                playerCollider.AddComponent(typeof(Rigidbody));
                playerCollider.GetComponent<Rigidbody>().useGravity = false;
                playerCollider.GetComponent<Rigidbody>().isKinematic = true;
                playerCollider.transform.gameObject.tag = "Player";

				AudioSource source = audioPlayer.GetComponent<AudioSource>();
				source.clip = music;
				source.loop = true;
				source.Play();

				Debug.Log("Success!");
				setup = true; //our work here is done
			}
		}
	}
}