using UnityEngine;
using System.Collections;

public enum DeathType {Slide, Fart, Stick, Deafult}; 

public class Death : AbstractBehavior {

	public GameObject deathGravityPrefab;
	public GameObject slidePrefab;
	public GameObject fartPrefab;
	public GameObject defaultPrefab;

	public float timeToRespawn;

	private Transform characterPosition;
	private Transform respawnPoint;

	private CameraController camController;

	void Awake() {
		camController = Camera.main.GetComponent<CameraController> ();
	}

	void OnEnable () {
		characterPosition = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		respawnPoint = GameObject.FindGameObjectWithTag ("Respawn").GetComponent<Transform>();

		GetComponent<Walk>().enabled = true;
		GetComponent<Jump>().enabled = true;
		GetComponent<Jump>().jumping = false;
		GetComponent<Fart>().enabled = false;
		GetComponent<Slide>().enabled = false;
		GetComponent<WallJump>().enabled = false;
	}

	void OnDisable () {
		Invoke ("RespawnCharacter", timeToRespawn);
	}

	public void KillCharacter(DeathType deathType) {
		gameObject.SetActive (false);

		Transform gravity = Instantiate (deathGravityPrefab).transform;

		Vector3 scaleDesired = new Vector3 (characterPosition.transform.localScale.x, 1, 1);
		Vector3 positionDesired = characterPosition.transform.position;

		gravity.transform.position = positionDesired;

		Transform deathTarget = gravity;

		GameObject deathResource = null;

		if (deathType == DeathType.Slide) deathResource = slidePrefab; //Morte Slide
		else if (deathType == DeathType.Fart) deathResource = fartPrefab; //Morte left//
		else deathResource = defaultPrefab;

		deathResource.transform.localScale = scaleDesired;
		deathResource.transform.position = positionDesired;

		Instantiate (deathResource).transform.parent = gravity;

		camController.target = deathTarget;
	}

	void RespawnCharacter() {
		gameObject.SetActive (true);

		characterPosition.position = respawnPoint.position;

		camController.target = characterPosition;
	}
}
