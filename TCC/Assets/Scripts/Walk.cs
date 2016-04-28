using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {
	public float speed = 5f;
	public float maxVelocity = 5f;
	bool right;
	bool left;

	public void Update() {
		right = inputState.GetButtonValue (inputButtons [0]);
		left = inputState.GetButtonValue (inputButtons [1]);
	}


	public void FixedUpdate() {
		if ((right || left) && !collisionState.onWall) {
			float tmpSpeed = speed;
			float velX = tmpSpeed * (float)inputState.direction;
			
			body2d.AddForce (new Vector2 (velX, 0));

			velocityX.CalculateMaxVelocity (maxVelocity);
		} 
		else {
			velocityX.BreakVelocity ();
		}
	}
}
