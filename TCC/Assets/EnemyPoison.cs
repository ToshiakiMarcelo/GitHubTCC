using UnityEngine;
using System.Collections;

public class EnemyPoison : MonoBehaviour {

	public string tagPlayer;

	public DeathType deathType;

	private bool alreadyDead;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Resource") {
			alreadyDead = true;
		}
		if (other.tag == tagPlayer && !alreadyDead) {
			Death death = other.GetComponent<Death> ();

			death.KillCharacter (deathType);
		}
	}
}