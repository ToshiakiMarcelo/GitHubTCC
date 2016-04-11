using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {
	//public    GameObject dustEffectPrefab;
	public 	  bool 		 jumping;
	public    float      jumpForce      = 8f;
	public    float		 jumpTime		= 0.1f;
	public    float      jumpDelay      = .1f;
	public    int        jumpCount      = 2;
	protected float      lastJumpTime   = 0;
	[SerializeField] protected int        jumpsRemaining = 0;

	protected virtual void Update () {
		bool  canJump  = inputState.GetButtonValue(inputButtons[0]);
		float holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

		if (collisionState.standing) {
			ToggleScripts (false);
			if (canJump && !jumping && holdTime < .1f) {
				jumpsRemaining = jumpCount - 1;
				StartCoroutine (JumpRoutine ());
			}
		} 
		else if (!collisionState.onWall) {
			if(canJump && !jumping && Time.time - lastJumpTime > jumpDelay && holdTime < .1f)
			{
				if (jumpsRemaining > 0) {
					jumpsRemaining--;
					StartCoroutine(JumpRoutine());
					Debug.Log ("DoubleJump");
				}

			}
		}
	}

	IEnumerator JumpRoutine()
	{
		jumping = true;
		body2d.velocity = new Vector2 (body2d.velocity.x,0);
		float timer = 0;

		bool holdJump = inputState.GetButtonValue (inputButtons [1]);

		while(holdJump && timer < jumpTime)
		{
			holdJump = inputState.GetButtonValue (inputButtons [1]);
			float proportionCompleted = timer / jumpTime;
			Vector2 thisFrameJumpVector = Vector2.Lerp(Vector2.up * jumpForce, Vector2.zero, proportionCompleted);
			body2d.AddForce(thisFrameJumpVector);
			timer += Time.deltaTime;
			yield return null;
		}

		jumping = false;

//		jumping = true;
//		float startGravity = body2d.gravityScale;
//		body2d.gravityScale = 0;
//		body2d.velocity = new Vector2 (body2d.velocity.x, jumpForce);
//		float timer = 0f;
//		lastJumpTime = Time.time;
//		bool holdJump = inputState.GetButtonValue (inputButtons [1]);
//
//		while (holdJump && timer < jumpTime)
//		{
//			timer += Time.deltaTime;
//			holdJump = inputState.GetButtonValue (inputButtons [1]);
//			yield return null;
//		}
//
//		if (startGravity != 0) body2d.gravityScale = startGravity;
//
//		jumping = false;
		ToggleScripts (true);
	}
}
