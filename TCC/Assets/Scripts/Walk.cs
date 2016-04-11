using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {
	public float speed         = 5f;
	protected float timer = 0;
	protected Directions directions;

	public void Start() {
		timer = 0;
		directions = inputState.direction;
	}

	public void FixedUpdate() {
		bool right = inputState.GetButtonValue(inputButtons[0]);
		bool left  = inputState.GetButtonValue(inputButtons[1]);

//		if (directions != inputState.direction) {
//			directions = inputState.direction;
//			timer = 0;
//		} 
//		else { 
//			
//		}

		if ((right || left) && !collisionState.onWall) {
			timer += Time.deltaTime;
			float holdTime = timer;

			float tmpSpeed = speed;
			float velX = tmpSpeed * (float)inputState.direction;

			if (timer >= 1)
				holdTime = 1;

			body2d.AddForce (new Vector2 ((velX - body2d.velocity.x) * 5, 0));
		} 
		else {
			body2d.velocity = new Vector2 (0, body2d.velocity.y);
		}
	}
}
