using UnityEngine;
using System.Collections;
using Spine.Unity;

public class AnimationFartDeath : MonoBehaviour {

	public GameObject fartAnimation;
	private SkeletonAnimation skeletonAnimation;

	void Awake() {
		skeletonAnimation = GetComponent<SkeletonAnimation> ();
	}
	
	// Update is called once per frame
	void Update () {
		;
		if (skeletonAnimation.state.GetCurrent (0) == null) {
			gameObject.SetActive (false);
			fartAnimation.SetActive (true);
		}
	}
}
