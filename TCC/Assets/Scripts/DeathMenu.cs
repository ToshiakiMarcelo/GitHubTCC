using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {

	private Transform characterPosition;
	private Image[] options;

	private bool right = false;
	private bool left = false;

	private Death death;

	void OnEnable() {
		characterPosition = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		death = characterPosition.GetComponent<Death> ();
		options = GetComponentsInChildren<Image> ();
	}

	void OnDisable() {
		bool state = false;

//		Transform deathTarget = null;
//
//		GameObject deathResource = null;

		DeathType deathType = DeathType.Slide;

		if (right) deathType = DeathType.Slide; //Morte Slide
		else if (left) deathType = DeathType.Fart; //Morte left//
		else state = true;

//		if (deathResource) {
//			Transform gravity = Instantiate (deathGravityPrefab).transform;
//
//			deathTarget = gravity;
//
//			Vector3 scaleDesired = characterPosition.transform.localScale;
//			Vector3 positionDesired = characterPosition.transform.position;
//
//			gravity.transform.position = positionDesired;
//
//			deathResource.transform.localScale = new Vector3 (scaleDesired.x, .5f, 1);
//			deathResource.transform.position = positionDesired;
//
//			Instantiate (deathResource).transform.parent = gravity;
//		}

		if (!state) death.KillCharacter (deathType);
	}

	void Update() {
		right = false;
		left = false;

		float rightValue;
		float leftValue;

		rightValue = Input.GetAxis ("Horizontal") > 0 ? Input.GetAxis ("Horizontal") : 0;
		leftValue = Input.GetAxis ("Horizontal") < 0 ? Input.GetAxis ("Horizontal") : 0;

		if (Input.GetButton ("Horizontal") && rightValue != 0) right = true;
		else if (Input.GetButton ("Horizontal") && leftValue != 0) left = true;
		else Invoke ("TimeToCancel", 0.5f);

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
