using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {
	public float speed         = 50f;
	public float runMultiplier = 2f;
	public bool  running;

	public void Update() {
		running    = false;
		bool right = inputState.GetButtonValue(inputButtons[0]);
		bool left  = inputState.GetButtonValue(inputButtons[1]);
//		bool run   = inputState.GetButtonValue(inputButtons[2]);
		float teste = inputState.GetButtonHoldTime (inputButtons [0]);

		if (right || left) {
			float tmpSpeed = speed;

//			if (run && runMultiplier > 0) {
//				tmpSpeed *= runMultiplier;
//				running   = true;
//			}

			float velX      = tmpSpeed * (float)inputState.direction;
			body2d.velocity = new Vector2(velX, body2d.velocity.y);
		}

		else {
			body2d.velocity = new Vector2(0,body2d.velocity.y);
		}
	}
}
