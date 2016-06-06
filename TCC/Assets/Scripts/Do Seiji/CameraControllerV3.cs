using UnityEngine;
using System.Collections;

public class CameraControllerV3 : MonoBehaviour {

	private Vector3 min;
	private Vector3 max;
	public  Transform target;
	public  InputState inputState;
	public  float dir;
	public  float distFromTargetX;
	public  float distFromTargetY;
	public  float smoothing = 3;
	public  float distanceToSmoothing = 3;
	public  BoxCollider2D cameraLimit;
	private Camera camera;
	private float distFromXOG;
	public  bool IsFollowing {get; set;}


	public void Start() {
		min = cameraLimit.bounds.min;
		max = cameraLimit.bounds.max;
		IsFollowing = target != null;

		distFromXOG = distFromTargetX;
		camera = GetComponent<Camera>();
	}

	public void LateUpdate() {
		float x = transform.position.x;
		float y = transform.position.y;
		if (inputState.direction == Directions.Right) dir = Mathf.Lerp(dir, 1, 5 * Time.deltaTime);
		if (inputState.direction == Directions.Left) dir = Mathf.Lerp(dir, -1, 5 * Time.deltaTime);

		if (IsFollowing) {
			if (Mathf.Abs(x - target.position.x) > 0) {
				if (Mathf.Abs (x - target.position.x) - distFromTargetX > distanceToSmoothing) x = Mathf.Lerp (x, target.position.x + distFromTargetX, smoothing * Time.deltaTime);
				else x = target.position.x + distFromTargetX * dir;
				//x = target.position.x + distFromTargetX;
			}

			if (Mathf.Abs (y - target.position.y) > 0) {
				if (Mathf.Abs (y - target.position.y) - distFromTargetY > distanceToSmoothing) y = Mathf.Lerp (y, target.position.y + distFromTargetY, smoothing * Time.deltaTime);
				else y = target.position.y + distFromTargetY * dir;
				//y = target.position.y + distFromTargetY;
			}
			float cameraHalfWidth = camera.orthographicSize * ((float) Screen.width / Screen.height);

			x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
			y = Mathf.Clamp(y, min.y + camera.orthographicSize, max.y - camera.orthographicSize);
			transform.position = new Vector3 (x, y, transform.position.z);
		}
	}
}
