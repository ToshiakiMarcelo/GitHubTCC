using UnityEngine;
using System.Collections;

public class WallJump : AbstractBehavior {
	public    bool    	jumpingOffWall;
	public 	  float 	jumpForce 	 = 3.5f;
	public    float		jumpTime	 = 0.5f;
	public 	  float 	speed        = 5f;
	protected Vector2  	jumpVelocity = new Vector2 ();
	protected float 	startGravity;

	void Update () {
		if (collisionState.onWall && !collisionState.standing) { //Colocar condicao caso esteja na parede com sangue
			bool canJump = inputState.GetButtonValue(inputButtons[0]);
			float holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

			if (canJump && !jumpingOffWall && holdTime == 0) {
				ToggleScripts (false);
				inputState.direction = inputState.direction == Directions.Right ? Directions.Left : Directions.Right;
				jumpVelocity = new Vector2 (speed, jumpForce);
				StartCoroutine (JumpWallRoutine());
			}
		}
	}

	IEnumerator JumpWallRoutine()
	{
		jumpingOffWall = true;
		body2d.velocity = Vector2.zero;
		float timer = 0;

		while(timer < jumpTime)
		{
			float proportionCompleted = timer / jumpTime;
			Vector2 thisFrameJumpVector = Vector2.Lerp(new Vector2 ((float)inputState.direction * speed, jumpForce), Vector2.zero, proportionCompleted);
			body2d.AddForce (thisFrameJumpVector);
			timer += Time.deltaTime;
			yield return null;
		}

		jumpingOffWall = false;
		ToggleScripts (true);
	}
}
