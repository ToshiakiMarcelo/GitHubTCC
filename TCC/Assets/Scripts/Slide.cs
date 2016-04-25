using UnityEngine;
using System.Collections;

public class Slide : AbstractBehavior {

	public float slipForce;
	public float valueToStopSlipping;
	public float maxSlipVelocity;
	private Directions initialDirection;

	public void Update() {
		ToggleScripts (false);
		initialDirection = inputState.direction;
		StartCoroutine (SlideForce ());
		this.enabled = false;
	}

	IEnumerator SlideForce() {
		float impulse = slipForce * Mathf.Abs(body2d.velocity.x);
		initialDirection = body2d.velocity.x > 0 ? Directions.Right : Directions.Left;
		float stopSlipping = 0;

		Walk walk = GetComponent<Walk> ();
		walk.actualDirection = initialDirection;

		while (impulse > 0) {
			Vector2 slipForceFrameVector = Vector2.right * impulse * (float)initialDirection;

			body2d.AddForce (slipForceFrameVector);
			impulse -= Time.deltaTime * stopSlipping;
			stopSlipping += valueToStopSlipping;

			velocityX.CalculateMaxVelocity (maxSlipVelocity);

			yield return null;
		}

		while (body2d.velocity.x >= walk.maxVelocity || body2d.velocity.x <= -walk.maxVelocity) {
			velocityX.BreakVelocity ();

			yield return null;
		}
		
		ToggleScripts (true);
	}
}
