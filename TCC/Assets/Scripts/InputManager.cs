using UnityEngine;
using System.Collections;

public enum Buttons { Right, Left, Up, Z, X}

public enum Condition { GreaterThan, LessThan }



[System.Serializable]
public class InputAxisState {
	public string    axisName;
	public float     offValue;
	public Buttons   button;
	public Condition condition;

	public bool value{
		get {
			float val = Input.GetAxis (axisName);
			switch (condition) {
			case Condition.GreaterThan:
				return val > offValue;
			case Condition.LessThan:
				return val < offValue;
			}
			return false;
		}
	}
}

public class InputManager : MonoBehaviour {
	public InputAxisState[] inputs;
	public InputState       inputState;

	public void Update() {
		for (int i = 0; i < inputs.Length; i++) {
			inputState.SetButtonValue (inputs [i].button, inputs [i].value);
		}

	}
}
