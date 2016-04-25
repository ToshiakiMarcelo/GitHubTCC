using UnityEngine;
using System.Collections;

public class ActionTrigger : MonoBehaviour {

	public string searchForTag;
	public enum Death {Slide, Fart, Stick}; 
	public Death death;

	// Use this for initialization
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == searchForTag) {
			if (death == Death.Slide)
				other.GetComponent<Slide> ().enabled = true;
			else if (death == Death.Slide)
				other.GetComponent<Slide> ().enabled = true; // Change to Fart
			else if (death == Death.Slide) 
				other.GetComponent<Slide> ().enabled = true; // Change to Stick
		}
	}
}
