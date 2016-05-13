using UnityEngine;
using System.Collections;

public class Respawn : AbstractBehavior {

	public int nbRespawn;
	public float cdRespawn;
	private bool canRespawn = true;
	private int actualNbRespawn;

	void OnEnable() {
		actualNbRespawn = nbRespawn;
	}

	void Update () {
		bool respawnButton = inputState.GetButtonValue (inputButtons [0]);
		float respawnButtonTime = inputState.GetButtonHoldTime (inputButtons [0]);

		if (respawnButton && respawnButtonTime < 0.1f && canRespawn && actualNbRespawn > 0) {
			Invoke ("Cooldown", cdRespawn);
			canRespawn = false;
			actualNbRespawn--;
			SetRespawnLocation ();
		}
	}

	void SetRespawnLocation () {
		if (collisionState.standing) {
			GameObject respawnPoint = GameObject.FindGameObjectWithTag ("Respawn");
			GameObject character = GameObject.FindGameObjectWithTag ("Player");

			respawnPoint.transform.position = character.transform.position;
		}
	}

	void Cooldown() {
		canRespawn = true;
	}
}
