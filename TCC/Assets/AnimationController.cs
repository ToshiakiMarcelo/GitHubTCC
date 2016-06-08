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

	void Start () {
		skeletonAnimation.state.Complete += delegate(Spine.AnimationState state, int trackIndex, int loopCount) {
//			Debug.Log(string.Format("anim {0} on track {1} completed its full duration. it looped {2} times", state, trackIndex, loopCount));
//			if (skeletonAnimation.state.ToString () == "Pouso") ChangeAnimationState("Idle");
		};

		skeletonAnimation.state.Event += delegate(Spine.AnimationState state, int trackIndex, Spine.Event e) {
//			Debug.Log(string.Format("anim {0} has endennded", state));
//			if (skeletonAnimation.state.ToString () == "Pouso") ChangeAnimationState("Idle");
		};
	}

	void Update () {
		if (body2d.velocity.y > 0 && jump.jumpsRemaining == 1) {
			if (skeletonAnimation.state.ToString () != "Pulo") ChangeAnimationState("Pulo");
		}
		else if (body2d.velocity.y > 0 && jump.jumpsRemaining == 0) {
			if (skeletonAnimation.state.ToString () != "Pulo Duplo") ChangeAnimationState("Pulo Duplo");
		}
		else if (body2d.velocity.y < 0) {
			if (skeletonAnimation.state.ToString () != "Pulo Queda") ChangeAnimationState("Pulo Queda");
		}
		else if (inputState.absVelX > 0) {
			if (skeletonAnimation.state.ToString () == "Pulo Queda") skeletonAnimation.state.SetAnimation (0, "Pouso", false);
			else if (skeletonAnimation.state.ToString () == "Pouso") skeletonAnimation.state.AddAnimation (0, "Walk", true, 0f);
			else if (skeletonAnimation.state.ToString () != "Walk") ChangeAnimationState("Walk");
		}
		else if (collisionState.standing) {
			if (skeletonAnimation.state.ToString () == "Pulo Queda") skeletonAnimation.state.SetAnimation (0, "Pouso", false);
			else if (skeletonAnimation.state.ToString () == "Pouso") skeletonAnimation.state.AddAnimation (0, "Idle", true, 0f);
			else if (skeletonAnimation.state.ToString () != "Idle") ChangeAnimationState ("Idle");
//			if (skeletonAnimation.state.ToString () != "Idle") ChangeAnimationState ("Idle");
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


