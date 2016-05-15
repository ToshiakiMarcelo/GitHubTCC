using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour {

	public bool isStanding;

	public GameObject shotPrefab;
	public float cdShot;

	private bool canGo = true;
	protected bool shotOutCamera = true;

	private bool shotOutCameraReference;

	void Update() {
		shotOutCameraReference = shotPrefab.GetComponent<Shot> ().shotOutCamera;

		shotOutCamera = shotOutCameraReference;
	}

	void FixedUpdate () {
		if (shotOutCamera) {
			GameObject shot = shotPrefab;

			shot.transform.position = new Vector2 (-10, 0);
		}

		if (canGo && shotOutCamera) {
			canGo = false;

			GameObject shot = shotPrefab;

			SpriteRenderer shotSprite = shot.GetComponent<SpriteRenderer> ();

			float halfWidthShot = (shotSprite.bounds.max.x - shotSprite.bounds.min.x)/2;
			float halfHeightShot = (shotSprite.bounds.max.y - shotSprite.bounds.min.y)/2;

			Vector3 positionDesired = Vector3.zero;
			Vector3 scaleDesired = Vector3.zero;
			Quaternion rotationDesired = transform.rotation;

			float direction = transform.localScale.x > 0 ? 1 : -1;
			scaleDesired = new Vector3 (transform.localScale.x, shot.transform.localScale.y, shot.transform.localScale.z); 

			if (isStanding) {
				float x = 0;

				if (transform.localScale.x > 0) {
					x = transform.GetComponent<SpriteRenderer> ().bounds.max.x + halfWidthShot;
					direction = 1;
				} else if (transform.localScale.x < 0) {
					x = transform.GetComponent<SpriteRenderer> ().bounds.min.x - halfWidthShot;
					direction = -1;
				}

				positionDesired = new Vector2 (x, transform.position.y);
			}
			else {
				float y = 0;

				if (transform.localScale.x > 0) {
					y = transform.GetComponent<SpriteRenderer> ().bounds.max.y + halfHeightShot;
					direction = 1;
				} else if (transform.localScale.x < 0) {
					y = transform.GetComponent<SpriteRenderer> ().bounds.min.y - halfHeightShot;
				}

				positionDesired = new Vector2 (transform.position.x, y);
			}

			shot.transform.localScale = scaleDesired;
			shot.transform.rotation = rotationDesired;
			shot.transform.position = positionDesired;

			shotOutCamera = false;
			shotPrefab.GetComponent<Shot> ().shotOutCamera = false;

			Invoke ("CooldownShot", cdShot);
		}
	}

	void CooldownShot(){
		canGo = true;
	}
}