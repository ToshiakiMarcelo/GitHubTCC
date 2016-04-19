using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {
	public float speed         = 5f;
	public float maxVelocity   = 5f;
	public float timer = 0;
	protected Directions directions;

//	void Awake() {
//		timer = 0;
//		Debug.Log("Teste");
//	}

	public void Start() {
		timer = 0;
		directions = inputState.direction;
	}

	public void FixedUpdate() {
		bool right = inputState.GetButtonValue(inputButtons[0]);
		bool left  = inputState.GetButtonValue(inputButtons[1]);

		if ((right || left) && !collisionState.onWall) {
			body2d.GetComponent<BoxCollider2D>().sharedMaterial.friction = 0;
			timer += Time.deltaTime;
			float holdTime = timer;

			if (timer >= 1)
				holdTime = 1;
			
			float tmpSpeed = speed;
			float velX = tmpSpeed * (float)inputState.direction;

			Debug.Log (holdTime);

			body2d.AddForce (new Vector2 (velX, 0));
			Vector2 v = body2d.velocity;
			v.x = Mathf.Clamp (v.x,-maxVelocity, maxVelocity);
			body2d.velocity = v;
			// ler right e left e resetar para zero no OnEnable
		} 
		else {
			timer = 0;
			body2d.GetComponent<BoxCollider2D> ().sharedMaterial.friction = 0.1f;
		}
	}
}
