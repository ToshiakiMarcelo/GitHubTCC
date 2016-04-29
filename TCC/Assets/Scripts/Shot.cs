using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public float speed;

	public string tagPlayer;
	public string tagCameraLimit;

	public DeathType deathType;

	private Rigidbody2D body2d;

	private bool vertical;
	private float direction;

	void Awake() {
		direction = transform.localScale.x;

		body2d = GetComponent<Rigidbody2D> ();

		if (transform.rotation.z != 0) vertical = true;
		else vertical = false;
	}

	void FixedUpdate() {
		if (vertical) {
			body2d.velocity = new Vector2(0, speed * direction) * Time.deltaTime;
			body2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}
		else {
			body2d.velocity = new Vector2(speed * direction, 0) * Time.deltaTime;
			body2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		}

		Debug.Log (body2d.velocity);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == tagPlayer) {
			Death death = other.GetComponent<Death> ();

			death.KillCharacter (deathType);
		}
	}

	void OnTriggerExit2D  (Collider2D other) {
		if (other.tag == tagCameraLimit) {
			Destroy (gameObject);
		}
	}
}
