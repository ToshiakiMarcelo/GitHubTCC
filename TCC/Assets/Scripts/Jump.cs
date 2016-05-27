﻿using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {
	//public    GameObject dustEffectPrefab;
	public bool jumping;
	public float jumpForce = 8f;
	public float jumpTime = 0.1f;
	public float jumpDelay = .1f;
	public int jumpCount = 2;
	protected float lastJumpTime = 0;
	[HideInInspector] public int jumpsRemaining = 0;

	protected virtual void Update () {
		bool  canJump  = inputState.GetButtonValue(inputButtons[0]);
		float holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

		WallJump wallJump = GetComponent<WallJump> ();

		if (collisionState.standing) {
			if (canJump && !jumping && holdTime == 0) {
				jumpsRemaining = jumpCount - 1;
				StartCoroutine (JumpRoutine ());
			}
		} 
		else if ((!collisionState.onWall && wallJump.enabled) || !wallJump.enabled) {
			if(canJump && !jumping && Time.time - lastJumpTime > jumpDelay && holdTime < .1f)
			{
				if (jumpsRemaining > 0) {
					jumpsRemaining--;
					StartCoroutine(JumpRoutine());

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
			timer += Time.fixedDeltaTime;

			yield return new WaitForFixedUpdate();
		}

		jumping = false;
	}
}
