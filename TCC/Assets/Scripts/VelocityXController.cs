﻿using UnityEngine;
using System.Collections;

public class VelocityXController : AbstractBehavior {
	public float breakVelocityValue = 2.5f; //15
	private Directions velocityDirection;
	public void Update() {
		Debug.Log (body2d.velocity.x);
		if (body2d.velocity.x > .001f)
			velocityDirection = Directions.Right;
		else if (body2d.velocity.x < - .001f)
			velocityDirection = Directions.Left;
	}

	public void CalculateMaxVelocity(float maxVelocity) {
		Vector2 v = body2d.velocity;
		v.x = Mathf.Clamp (v.x, -maxVelocity, maxVelocity);
		body2d.velocity = v;
	}

	public void BreakVelocity() {
		if (velocityDirection == Directions.Right) {
			if (body2d.velocity.x > 0){
				Debug.Log ("first");
				body2d.velocity -= new Vector2 (Mathf.Clamp(body2d.velocity.x, 0, breakVelocityValue * Time.deltaTime), 0);
				}
			else{
				Debug.Log ("second");
				body2d.velocity = Vector2.up * body2d.velocity.y;
			}
		}
		else {
			if (body2d.velocity.x < 0)
				body2d.velocity += new Vector2 (Mathf.Clamp(body2d.velocity.x, breakVelocityValue * Time.deltaTime, 0), 0);
			else
				body2d.velocity = Vector2.up * body2d.velocity.y;
		}
	}
}
