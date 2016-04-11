using UnityEngine;
using System.Collections;

public class WallJump : AbstractBehavior {
	public    bool    	jumpingOffWall;
	public 	  float 	jumpForce 	 = 3.5f;
	public    float		jumpTime	 = 0.5f;
	public 	  float 	speed        = 5f;
	protected Vector2  	jumpVelocity = new Vector2 ();
	protected float 	startGravity;
	protected Jump 		jump;
	protected Walk      walk;

	void Start () {
	}

	void Update () {
		if (collisionState.onWall && !collisionState.standing) { //Colocar condicao caso esteja na parede com sangue
			bool canJump = inputState.GetButtonValue(inputButtons[0]);
			float holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

			if (canJump && !jumpingOffWall && holdTime < .1f) {
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
		float startGravity = body2d.gravityScale;
		body2d.gravityScale = 0;
		body2d.velocity = new Vector2(jumpVelocity.x * (float)inputState.direction, jumpVelocity.y);
		float timer = 0f;

		while (timer < jumpTime)
		{
			timer += Time.deltaTime;
			yield return null;
		}

		if (startGravity != 0) body2d.gravityScale = startGravity;

		jumpingOffWall = false;
		ToggleScripts (true);
	}
}
