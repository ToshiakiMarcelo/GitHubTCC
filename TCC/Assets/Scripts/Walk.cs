using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {
	public float speed = 5f;
	public float maxVelocity = 5f;
	[HideInInspector] public Directions actualDirection;

	public void FixedUpdate() {
		bool right = inputState.GetButtonValue (inputButtons [0]);
		bool left = inputState.GetButtonValue (inputButtons [1]);
		Debug.Log (actualDirection);
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

	public void ChangeDirection(){
		actualDirection = inputState.direction;
	}
}
