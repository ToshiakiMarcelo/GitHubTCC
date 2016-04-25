using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {
	public float speed = 5f;
	public float maxVelocity = 5f;
	protected Directions directions;

	public void Start() {
		directions = inputState.direction;
	}

	public void FixedUpdate() {
		bool right = inputState.GetButtonValue (inputButtons [0]);
		bool left = inputState.GetButtonValue (inputButtons [1]);

		if ((right || left) && !collisionState.onWall) {
			float tmpSpeed = speed;
			float velX = tmpSpeed * (float)inputState.direction;
			
			body2d.AddForce (new Vector2 (velX, 0));

			velocityX.CalculateMaxVelocity (maxVelocity);
		} 
		else {
			velocityX.BreakVelocity (inputState.direction);
		}
	}
}
