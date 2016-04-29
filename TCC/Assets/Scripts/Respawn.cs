using UnityEngine;
using System.Collections;

public class Respawn : AbstractBehavior {


	void Update () {
		bool respawnButton = inputState.GetButtonValue (inputButtons [0]);
		float respawnButtonTime = inputState.GetButtonHoldTime (inputButtons [0]);

		if (respawnButton && respawnButtonTime < .1f) {
			Invoke ("SetRespawnLocation", 0.1f);
		}
	}

	void SetRespawnLocation () {
		if (collisionState.standing) {
			GameObject respawnPoint = GameObject.FindGameObjectWithTag ("Respawn");
			GameObject character = GameObject.FindGameObjectWithTag ("Player");

			respawnPoint.transform.position = character.transform.position;
		}
	}
}
