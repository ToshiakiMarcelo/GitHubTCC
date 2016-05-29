using UnityEngine;
using System.Collections;

public class StickDeathConfig : MonoBehaviour {

	void Start() {
		GetComponentInParent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;

	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log (other.tag);
		if (other.tag == "EnemyPoison") {
			if (transform.position.x > other.transform.position.x) {
				transform.position = new Vector2 (other.bounds.max.x, other.transform.position.y);
				transform.localScale = new Vector3 (1, 3, 1);
			} 
			else {
				transform.position = new Vector2 (other.bounds.min.x, other.transform.position.y);
				transform.localScale = new Vector3 (-1, 3, 1);
			}
		}
	}
}
