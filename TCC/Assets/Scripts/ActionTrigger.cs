using UnityEngine;
using System.Collections;

public class ActionTrigger : MonoBehaviour {

	public string searchForTag;
	public DeathType death;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == searchForTag) {
			if (death == DeathType.Slide) {
				if (other.GetComponent<CollisionState> ().standing) {
					other.GetComponent<Slide> ().enabled = true;
				}
			} else if (death == DeathType.Fart) {
				if (!other.GetComponent<CollisionState> ().standing && other.transform.position.y > transform.GetComponent<BoxCollider2D> ().bounds.max.y) {
					other.GetComponent<Fart> ().direction = transform.localScale.x;
					other.GetComponent<Fart> ().enabled = true;
				}
			} 
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == searchForTag) {
			if (death == DeathType.Stick) {
				other.GetComponent<WallJump> ().enabled = true; // Change to Stick
				if (other.GetComponent<CollisionState> ().onWall) {
					if (transform.localScale.x == -1 && other.GetComponent<InputState> ().direction == Directions.Right) {
						other.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
					} else if (transform.localScale.x == 1 && other.GetComponent<InputState> ().direction == Directions.Left) {
						other.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
					}
				} else {
					other.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == searchForTag) {
			if (death == DeathType.Stick) {
				other.GetComponent<WallJump> ().enabled = false;
			}
		}
	}
}
