using UnityEngine;
using System.Collections;

public class ActionTrigger : MonoBehaviour {

	public string searchForTag;
	public DeathType death;

	// Use this for initialization
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == searchForTag) {
			if (death == DeathType.Slide) {
				if (other.GetComponent<CollisionState> ().standing) {
					other.GetComponent<Slide> ().enabled = true;
				}
			} else if (death == DeathType.Fart) {
				Debug.Log ("peido");
				if (!other.GetComponent<CollisionState> ().standing && other.transform.position.y > transform.GetComponent<BoxCollider2D>().bounds.max.y) {
					other.GetComponent<Fart> ().direction = transform.localScale.x;
					other.GetComponent<Fart> ().enabled = true;
				}
			}
			else if (death == DeathType.Stick) {
				other.GetComponent<Slide> ().enabled = true; // Change to Stick
			}
		}
	}
}
