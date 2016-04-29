using UnityEngine;
using System.Collections;

public class CollisionState : MonoBehaviour {
	public  LayerMask  collisionLayer;
	public  bool       standing;
	public  bool       onWall;
	public  Vector2    bottomPosition      = Vector2.zero;
	public  Vector2    rightPosition       = Vector2.zero;
	public  Vector2    leftPosition        = Vector2.zero;
	public  float      collisionRadius     = 0f;
	public  Color      debugCollisionColor = Color.red;
	private InputState inputState;

	public void Awake() {
		inputState = GetComponent<InputState>();
	}

	public void FixedUpdate() {
		Vector2 pos = bottomPosition;

		pos.x += transform.position.x;
		pos.y += transform.position.y;

		standing = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

		pos = inputState.direction == Directions.Right ? rightPosition : leftPosition;
		pos.x += transform.position.x;
		pos.y += transform.position.y;
		
		onWall = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);
	}

	public void OnDrawGizmos() {
		Gizmos.color = debugCollisionColor;

		var positions = new Vector2[] { rightPosition, bottomPosition, leftPosition };

		for (int i = 0; i < positions.Length; i++) {
			var pos = positions[i];
			pos.x  += transform.position.x;
			pos.y  += transform.position.y;

			Gizmos.DrawWireSphere(pos, collisionRadius);
		}
	}
}
