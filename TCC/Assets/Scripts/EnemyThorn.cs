using UnityEngine;
using System.Collections;
using Spine.Unity;

public class EnemyThorn : MonoBehaviour {

	private SkeletonAnimation skeletonAnimation;

	public float cdToAttack = 5;

	public string tagPlayer;

	public DeathType deathType;

	private bool canGo = true;

	private BoxCollider2D boxCollider;
	private Vector3 originalPosition;

	void Awake() {
		skeletonAnimation = GetComponentInChildren<SkeletonAnimation> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		originalPosition = boxCollider.transform.position;
	}
		
	public void  FixedUpdate () {
		if (boxCollider.transform.position.x >= originalPosition.x) {
			boxCollider.transform.position -= new Vector3(.0001f,0,0);
		}
		else {
			boxCollider.transform.position += new Vector3(.0001f,0,0);
		}


		if (canGo) {
			if (skeletonAnimation.state.ToString () == "InimigoEspinhoIdleFechado")
				skeletonAnimation.state.SetAnimation (0, "InimigoEspinhoAbre", false);
			else if (skeletonAnimation.state.GetCurrent (0) == null) {
				Invoke ("CooldownShot", cdToAttack);
				ChangeAnimationState ("InimigoEspinhoIdleAberto");
			}
		} 
		else {
			if (skeletonAnimation.state.ToString () == "InimigoEspinhoIdleAberto")
				skeletonAnimation.state.SetAnimation (0, "InimigoEspinhoFecha", false);
			else if (skeletonAnimation.state.GetCurrent (0) == null) {
				Invoke ("CooldownShot", cdToAttack);
				ChangeAnimationState ("InimigoEspinhoIdleFechado");
			}
		}
	}

	void CooldownShot(){
		if (!canGo)
			canGo = true;
		else
			canGo = false;
	}

	public void ChangeAnimationState(string animation){
		skeletonAnimation.state.SetAnimation(0, animation, true);
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == tagPlayer && canGo) {
			Death death = other.GetComponent<Death> ();

			death.KillCharacter (deathType);
		}
	}
}
