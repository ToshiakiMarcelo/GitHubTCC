using UnityEngine;
using System.Collections;

public class TesteInputs : AbstractBehavior {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		bool right = inputState.GetButtonValue(inputButtons[0]);
		bool left  = inputState.GetButtonValue(inputButtons[1]);
		bool jump   = inputState.GetButtonValue(inputButtons[2]);
		bool suicide = inputState.GetButtonValue(inputButtons[3]);
		bool checkPoint  = inputState.GetButtonValue(inputButtons[4]);
	}
}
