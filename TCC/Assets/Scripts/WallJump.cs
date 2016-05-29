using UnityEngine;
using System.Collections;

public class WallJump : AbstractBehavior {
	public    bool    	jumpingOffWall;
	public 	  float 	jumpForce 	 = 3.5f;
	public    float		jumpTime	 = 0.5f;
	public 	  float 	speed        = 5f;
	public float maxVelocity = 5f;
	protected Vector2  	jumpVelocity = new Vector2 ();

	void Update () {
		if (collisionState.onWall && !collisionState.standing) { //Colocar condicao caso esteja na parede com sangue
			bool canJump = inputState.GetButtonValue(inputButtons[0]);
			float holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

			Jump jump = GetComponent<Jump> ();

			if (canJump && !jumpingOffWall && holdTime == 0) {
				Debug.Log ("WallJUmP");
				jump.jumpsRemaining = 1;
				ToggleScripts (false);
				inputState.direction = inputState.direction == Directions.Right ? Directions.Left : Directions.Right;
				StartCoroutine (JumpWallRoutine());
			}
		}
	}

	IEnumerator JumpWallRoutine()
	{
		jumpingOffWall = true;
		body2d.velocity = Vector2.zero;
		float timer = 0;

		Walk walk = GetComponent<Walk> ();

		while(timer < jumpTime)
		{
			float proportionCompleted = timer / jumpTime;
			Vector2 thisFrameJumpVector = Vector2.Lerp(new Vector2 ((float)inputState.direction  * speed, jumpForce), Vector2.zero, proportionCompleted);
			body2d.AddForce (thisFrameJumpVector);
			timer += Time.deltaTime;

			velocityX.CalculateMaxVelocity (maxVelocity);

			yield return new WaitForFixedUpdate();
		}

		while (body2d.velocity.x >= walk.maxVelocity
			|| body2d.velocity.x <= -walk.maxVelocity) {
			velocityX.BreakVelocity ();

			yield return new WaitForFixedUpdate();
		}

		jumpingOffWall = false;
		ToggleScripts (true);
	}
}
