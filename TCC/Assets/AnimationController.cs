using UnityEngine;
using System.Collections;
using Spine.Unity;

public class AnimationController : MonoBehaviour {

	private SkeletonAnimation skeletonAnimation;
	private CollisionState collisionState;
	private InputState inputState;
	private Rigidbody2D body2d;
	private Jump jump;

	void Awake() {
		skeletonAnimation = GetComponent<SkeletonAnimation> ();
		collisionState = GetComponentInParent<CollisionState>();
		inputState = GetComponentInParent<InputState>();
		body2d = GetComponentInParent<Rigidbody2D> ();
		jump = GetComponentInParent<Jump> ();
	}

	void Update () {
		



		if (body2d.velocity.y > 0 && jump.jumpsRemaining == 1) {
			if (skeletonAnimation.state.ToString () != "Pulo") ChangeAnimationState("Pulo");
		}
		else if (body2d.velocity.y > 0 && jump.jumpsRemaining == 0) {
			if (skeletonAnimation.state.ToString () != "PuloDuplo2") ChangeAnimationState("PuloDuplo2");
		}
		else if (body2d.velocity.y < 0) {
			if (skeletonAnimation.state.ToString () != "PuloQueda") ChangeAnimationState("PuloQueda");
		}
		else if (inputState.absVelX > 0) {
			if (skeletonAnimation.state.ToString () != "Walk") ChangeAnimationState("Walk");
		}
		else if (collisionState.standing) {
			if (skeletonAnimation.state.ToString () != "Idle") ChangeAnimationState ("Idle");
		}
	}

	public void ChangeAnimationState(string animation){
		skeletonAnimation.state.SetAnimation(0, animation, true);
	}
}

//private InputState     inputState;
//private Walk           walkBehavior;
//private Animator       animator;
//private CollisionState collisionState;
//private Duck           duckBehavior;
//
//public void Awake() {
//	inputState     = GetComponent<InputState>();
//	walkBehavior   = GetComponent<Walk>();
//	animator       = GetComponent<Animator>();
//	collisionState = GetComponent<CollisionState>();
//	duckBehavior   = GetComponent<Duck>();
//}
//
//public void Update() {
//	
//
//	if (inputState.absVelX > 0) {
//		ChangeAnimationState(1);
//	}
//
//	if (inputState.absVelY > 0) {
//		ChangeAnimationState(2);
//	}
//
//	animator.speed = walkBehavior.running ? walkBehavior.runMultiplier : 1;
//
//	if (duckBehavior.ducking) {
//		ChangeAnimationState(3);
//	}
//
//	if (!collisionState.standing && collisionState.onWall) {
//		ChangeAnimationState(4);
//	}
//}


