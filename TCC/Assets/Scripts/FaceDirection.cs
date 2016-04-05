using UnityEngine;
using System.Collections;

public class FaceDirection : AbstractBehavior {

	public void Update() {
		bool right = inputState.GetButtonValue(inputButtons[0]);
		bool left  = inputState.GetButtonValue(inputButtons[1]);

		if (right) {
			inputState.direction = Directions.Right;
		} else if (left) {
			inputState.direction = Directions.Left;
		}

		transform.localScale = new Vector3((float)inputState.direction, 1, 1);
	}
}
