using UnityEngine;
using System.Collections;

public class CameraLimitController : MonoBehaviour {
	
	public string tagCameraLimit;
	public DeathType deathType;

	void OnTriggerExit2D  (Collider2D other) {
		if (other.tag == tagCameraLimit) {
			Death death = GetComponent<Death> ();
			death.KillCharacter (deathType);
		}
	}
}
