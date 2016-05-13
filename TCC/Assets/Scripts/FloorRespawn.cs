using UnityEngine;
using System.Collections;

public class FloorRespawn : MonoBehaviour {

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Player") {
			
			float halfSize = other.bounds.size.x / 2;
			if (other.transform.position.x < gameObject.GetComponent<BoxCollider2D> ().bounds.max.x &&
			    other.transform.position.x > gameObject.GetComponent<BoxCollider2D> ().bounds.min.x) {
				other.GetComponent<Respawn> ().enabled = true;
			} 
			else {
				other.GetComponent<Respawn> ().enabled = false;
			}
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			other.GetComponent<Respawn> ().enabled = false;
		}
	}
}