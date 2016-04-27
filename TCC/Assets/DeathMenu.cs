using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {

	public GameObject slidePrefab;
	public GameObject fartPrefab;

	private Transform characterPosition;
	//private Vector3 positionDesired;
	private Image[] options;

	private bool right = false;
	private bool left = false;

	private Transform respawnPoint;

	void OnEnable() {
		characterPosition = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		respawnPoint = GameObject.FindGameObjectWithTag ("Respawn").GetComponent<Transform>();
		options = GetComponentsInChildren<Image> ();
	}

	void OnDisable() {
		bool state = false;

		GameObject deathResource = null;

		if (right) deathResource = slidePrefab; //Morte Slide
		else if (left) deathResource = fartPrefab; //Morte left//
		else state = true;

		if (deathResource) {
			Vector3 scaleDesired = characterPosition.transform.localScale;
			Vector3 positionDesired = characterPosition.transform.position;

			deathResource.transform.localScale = new Vector3 (scaleDesired.x, .5f, 1);
			Instantiate (deathResource).transform.position = positionDesired;
		}

		if (!state) characterPosition.position = respawnPoint.position;
	}

	void Update() {
		//positionDesired = character.transform.position;

		right = false;
		left = false;

		float rightValue;
		float leftValue;

		rightValue = Input.GetAxis ("Horizontal") > 0 ? Input.GetAxis ("Horizontal") : 0;
		leftValue = Input.GetAxis ("Horizontal") < 0 ? Input.GetAxis ("Horizontal") : 0;

		if (Input.GetButton("Horizontal") && rightValue != 0) right = true;
		if (Input.GetButton("Horizontal") && leftValue != 0) left = true;

		for (int i = 0; i < options.Length; i++) options [i].color = Color.white;

		if (right) options [1].color = Color.red;
		else if (left) options [2].color = Color.red;
		else options [0].color = Color.red;

		//if (right)
		//Debug.Log ("verdade");

//		bool up;
//		bool down;
//
//		float upValue;
//		float downValue;

	}
}
