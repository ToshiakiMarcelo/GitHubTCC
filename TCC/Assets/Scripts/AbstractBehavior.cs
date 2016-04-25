using UnityEngine;
using System.Collections;

public abstract class AbstractBehavior : MonoBehaviour {
	public    Buttons[] inputButtons;
	public    MonoBehaviour[] dissableScripts;
	protected InputState inputState;
	protected Rigidbody2D body2d;
	protected CollisionState  collisionState;
	protected VelocityXController velocityX;

	protected virtual void Awake(){
		inputState = GetComponent<InputState> ();
		body2d = GetComponent<Rigidbody2D> ();
		collisionState = GetComponent<CollisionState> ();
		velocityX = GetComponent<VelocityXController> ();
	}

	protected virtual void ToggleScripts(bool value){
		for (int i = 0; i < dissableScripts.Length; i++)
			dissableScripts[i].enabled = value;
	}
}
