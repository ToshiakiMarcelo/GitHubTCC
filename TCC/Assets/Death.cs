using UnityEngine;
using System.Collections;

public class Death : AbstractBehavior {

	public GameObject deathMenu;

	void Update () {
		bool deathButton = inputState.GetButtonValue (inputButtons [0]);

		if (deathButton) {
			StartCoroutine (SlownDeath());
			ToggleScripts (false);
			enabled = false;
		}
	}

	IEnumerator SlownDeath() {
		while (Input.GetButton("Fire1")) {
			deathMenu.SetActive(true);
			Time.timeScale = 0.0000000001f;

			yield return null;
		}
			
		while (!Input.GetButton ("Fire1") && !enabled) {
			Time.timeScale = 1;

			enabled = true;
			deathMenu.SetActive(false);
			ToggleScripts (true);

			yield return null;
		}

	}
}
