using UnityEngine;
using System.Collections;

public class EnemyThorn : MonoBehaviour {

	public string tagPlayer;

	public DeathType deathType;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == tagPlayer) {
			Death death = other.GetComponent<Death> ();

			death.KillCharacter (deathType);
		}
	}
}
