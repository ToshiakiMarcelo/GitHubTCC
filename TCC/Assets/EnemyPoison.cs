using UnityEngine;
using System.Collections;

public class EnemyPoison : MonoBehaviour {

	public string tagPlayer;

	public DeathType deathType;

	private bool AlreadyDead;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Resource") {
			AlreadyDead = true;
		}
		if (other.tag == tagPlayer && !AlreadyDead) {
			Death death = other.GetComponent<Death> ();

			death.KillCharacter (deathType);
		}
	}
}