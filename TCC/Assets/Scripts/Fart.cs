using UnityEngine;
using System.Collections;

public class Fart : AbstractBehavior {
	public float jumpForce = 3.5f;
	public float jumpTime = 0.2f;
	public float speed = 5f;
	public float maxVelocity = 5f;
	[HideInInspector] public float direction;

	public void Update() {
		ToggleScripts (false);
		StartCoroutine (FartImpulse ());
		this.enabled = false;
	}

	IEnumerator FartImpulse()
	{
		body2d.velocity = Vector2.zero;
		float timer = 0;

		Walk walk = GetComponent<Walk> ();
		inputState.direction = direction == 1 ? Directions.Right : Directions.Left;

		while(timer < jumpTime)
		{
			Debug.Log (timer);
			float proportionCompleted = timer / jumpTime;
			Vector2 thisFrameJumpVector = Vector2.Lerp(new Vector2 (direction * speed, jumpForce), Vector2.zero, proportionCompleted);
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

		ToggleScripts (true);
	}
}
