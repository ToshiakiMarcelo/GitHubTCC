using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
	public LayerMask  collisionLayer;
	public bool standing;
	public float gravityVelocity;
	public Vector2 bottomPosition = Vector2.zero;
	public float collisionRadius = 0f;
	public Color debugCollisionColor = Color.red;
	private bool isFalling = false;

	public void FixedUpdate() {
		Vector2 pos = bottomPosition;

		pos.x += transform.position.x;
		pos.y += transform.position.y;

		standing = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

		if (standing) isFalling = false;
		else isFalling = true;

		if (isFalling) transform.position -= Vector3.up * gravityVelocity * Time.deltaTime;
	}

	public void OnDrawGizmos() {
		Gizmos.color = debugCollisionColor;

		var positions = new Vector2[] {bottomPosition};

		for (int i = 0; i < positions.Length; i++) {
			var pos = positions[i];
			pos.x  += transform.position.x;
			pos.y  += transform.position.y;

			Gizmos.DrawWireSphere(pos, collisionRadius);
		}
	}
}
