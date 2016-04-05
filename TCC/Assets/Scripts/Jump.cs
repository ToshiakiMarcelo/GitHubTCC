using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {
	public    GameObject dustEffectPrefab;
	public    float      jumpSpeed      = 200f;
	public    float      jumpDelay      = .1f;
	public    int        jumpCount      = 2;
	protected float      lastJumpTime   = 0;
	protected int        jumpsRemaining = 0;
	
	protected virtual void Update () {
		bool  canJump  = inputState.GetButtonValue(inputButtons[0]);
		float holdTime = inputState.GetButtonHoldTime(inputButtons[0]);
		//Debug.Log (holdTime);
//		if (collisionState) {
		if (canJump && holdTime <= .5f) {
			Vector2 vel = body2d.velocity;
			body2d.velocity = new Vector2 (vel.x, jumpSpeed);
			//jumpsRemaining = jumpCount - 1;
			//OnJump();
		}
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
	}

	protected virtual void OnJump() {
		Vector2 vel     = body2d.velocity;
		lastJumpTime    = Time.time;
		body2d.velocity = new Vector2(vel.x, jumpSpeed);
	}
}
