using UnityEngine;
using System.Collections;
using Spine.Unity;

public class BoggyAnimMnager : MonoBehaviour
{
	private InputState inputStateRef;
	private Walk walkRef;
	private Jump jumpRef;

	private SkeletonAnimation skelAnim;

	void Start ()
	{
		inputStateRef = GetComponent<InputState>();
		walkRef = GetComponent<Walk>();
		jumpRef = GetComponent<Jump>();

		skelAnim = GetComponent<SkeletonAnimation>();

		// para custom events
		skelAnim.state.Event += HandleEventDelegate;

		// inicio de animação
		skelAnim.state.Start += delegate(Spine.AnimationState state, int trackIndex) {
			Debug.Log(string.Format("animation {0} started a new animation.", state));
		};

		// quando uma animação completa seu ciclo (não importa se loopa ou não)
		skelAnim.state.Complete += delegate(Spine.AnimationState state, int trackIndex, int loopCount) {
			Debug.Log(string.Format("animation {0} completed its full duration. it looped {1} times", state, loopCount));
		};

		// quando uma animação é interrompida (atravez de state.AddAnimation() )
		skelAnim.state.End += delegate(Spine.AnimationState state, int trackIndex) {
			Debug.Log(string.Format("animation {0} ended!", state));
		};
	}

	// isso é para custom eventos criados no spine para, por exemplo, footsteps
	void HandleEventDelegate (Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		Debug.Log("ye");
	}

	public void Update()
	{
		
	}

	public void FixedUpdate()
	{
		if (inputStateRef.absVelX == 0)
		{
			skelAnim.loop = true;
			skelAnim.AnimationName = "Idle";
		}

		if (inputStateRef.absVelX > 0)
		{
			skelAnim.loop = true;
			skelAnim.AnimationName = "Walk";
		}

//		if ()
//		{
//			skelAnim.loop = false;
//			skelAnim.AnimationName = "Morte Deslize";
//		}
	}
}
