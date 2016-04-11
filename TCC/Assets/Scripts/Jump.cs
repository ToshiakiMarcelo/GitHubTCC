using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {
	public    GameObject dustEffectPrefab;
	public    float      jumpForce      = 3.5f;
	public    float		 jumpTime		= 0.5f;
	public    float      jumpDelay      = .1f;
	public    int        jumpCount      = 2;
	protected float      lastJumpTime   = 0;
	protected int        jumpsRemaining = 0;

	bool jumping;
	bool holdJump;

	protected virtual void Update () {
		bool  canJump  = inputState.GetButtonValue(inputButtons[0]);
		holdJump = inputState.GetButtonValue (inputButtons [1]);
		float holdTime = inputState.GetButtonHoldTime(inputButtons[0]);
		//Debug.Log (holdTime);
//		if (collisionState) {
//		if (canJump && holdTime <= .5f) {
//			Vector2 vel = body2d.velocity;
//			body2d.velocity = new Vector2 (vel.x, jumpSpeed);
//			//jumpsRemaining = jumpCount - 1;
//			//OnJump();
//		}
//		} 
//			if (canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay) {
//				if (jumpsRemaining > 0) {
//					OnJump();
//					jumpsRemaining--;
//					var clone                = Instantiate(dustEffectPrefab);
//					clone.transform.position = transform.position;
//				}
//			}
//		}
		if (collisionState.standing) {
			if (canJump && !jumping) {
				jumpsRemaining = jumpCount - 1;
				StartCoroutine (JumpRoutine ());
				jumping = true;
			}
		} 
		else {
			if(canJump && !jumping && Time.time - lastJumpTime > jumpDelay && holdTime < .1f)
			{
				jumping = false;
				if (jumpsRemaining > 0) {
					jumpsRemaining--;
					jumping = true;
					StartCoroutine(JumpRoutine());
				}

			}
		}

	}

//	protected virtual void OnJump() {
//		Vector2 vel     = body2d.velocity;
//		lastJumpTime    = Time.time;
//		body2d.velocity = new Vector2(vel.x, jumpSpeed);
//	}

	IEnumerator JumpRoutine()
	{
//		//Add force on the first frame of the jump
//		body2d.velocity = Vector2.zero;
//		body2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//
//		//Wait while the character's y-velocity is positive (the character is going
//		//up)
//		while(holdJump && body2d.velocity.y > 0)
//		{
//			yield return null;
//		}
//
//		//If the jumpButton is released but the character's y-velocity is still
//		//positive...
//		if (body2d.velocity.y > 0) 
//		{
//			//...set the character's y-velocity to 0;
//			body2d.velocity = new Vector2 (body2d.velocity.x, 0);
//		}
//
//		jumping = false;

//		body2d.velocity = Vector2.zero;
//		float timer = 0;
//		lastJumpTime = Time.time;
//
//		while(holdJump && timer < jumpTime)
//		{
//			//Calculate how far through the jump we are as a percentage
//			//apply the full jump force on the first frame, then apply less force
//			//each consecutive frame
//
//			float proportionCompleted = timer / jumpTime;
//			Vector2 thisFrameJumpVector = Vector2.Lerp(Vector2.up * jumpForce, Vector2.zero, proportionCompleted);
//			body2d.AddForce(thisFrameJumpVector);
//			timer += Time.deltaTime;
//			yield return null;
//		}
//
//		jumping = false;

		float startGravity = body2d.gravityScale;
		body2d.gravityScale = 0;
		body2d.velocity = Vector2.up * jumpForce;
		float timer = 0f;

		while (holdJump && timer < jumpTime)
		{
			timer += Time.deltaTime;
			yield return null;
		}

		body2d.gravityScale = startGravity;

		jumping = false;
	}
}
