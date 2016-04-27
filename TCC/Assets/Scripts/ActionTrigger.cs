using UnityEngine;
using System.Collections;

public class ActionTrigger : MonoBehaviour {

	public string searchForTag;
	public enum Death {Slide, Fart, Stick}; 
	public Death death;

	// Use this for initialization
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == searchForTag) {
			if (death == Death.Slide) {
				if (other.GetComponent<CollisionState> ().standing) {
					other.GetComponent<Slide> ().enabled = true;
				}
			} else if (death == Death.Fart) {
				Debug.Log ("peido");
				if (!other.GetComponent<CollisionState> ().standing && other.transform.position.y > transform.GetComponent<BoxCollider2D>().bounds.max.y) {
					other.GetComponent<Fart> ().direction = transform.localScale.x;
					other.GetComponent<Fart> ().enabled = true;
				}
			}
			else if (death == Death.Stick) {
				other.GetComponent<Slide> ().enabled = true; // Change to Stick
			}
		}
	}
}
