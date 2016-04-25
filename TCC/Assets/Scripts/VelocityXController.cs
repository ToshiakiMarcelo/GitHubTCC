using UnityEngine;
using System.Collections;

public class VelocityXController : AbstractBehavior {
	public float breakVelocityValue = 2.5f; //15
	private Directions velocityDirection;
	public void Update() {
		if (body2d.velocity.x > 0)
			velocityDirection = Directions.Right;
		else
			velocityDirection = Directions.Left;
	}

	public void CalculateMaxVelocity(float maxVelocity) {
		Vector2 v = body2d.velocity;
		v.x = Mathf.Clamp (v.x, -maxVelocity, maxVelocity);
		body2d.velocity = v;
	}

	public void BreakVelocity() {
		if (velocityDirection == Directions.Right) {
			if (body2d.velocity.x > 0)
				body2d.velocity -= new Vector2 (breakVelocityValue * Time.deltaTime, 0);
			else
				body2d.velocity = Vector2.up * body2d.velocity.y;
		}
		else {
			if (body2d.velocity.x < 0)
				body2d.velocity += new Vector2 (breakVelocityValue * Time.deltaTime, 0);
			else
				body2d.velocity = Vector2.up * body2d.velocity.y;
		}
	}
}
