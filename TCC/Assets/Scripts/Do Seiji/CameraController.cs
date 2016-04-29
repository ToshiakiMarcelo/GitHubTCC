using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Vector3 min;
	private Vector3 max;
	public  Transform target;
	public  float distFromTargetX;
	public  float distFromTargetY;
	//public  Vector2 margin;
	public  float smoothing;
	public  float distanceToSmoothing;
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

		if (IsFollowing) {
			if (Mathf.Abs(x - target.position.x) > 0) {
				if (Mathf.Abs (x - target.position.x) > distanceToSmoothing) x = Mathf.Lerp (x, target.position.x + distFromTargetX, smoothing * Time.deltaTime);
				else x = target.position.x + distFromTargetX;
				//x = target.position.x + distFromTargetX;
			}

			if (Mathf.Abs (y - target.position.y) > 0) {
				if (Mathf.Abs (y - target.position.y) > distanceToSmoothing) y = Mathf.Lerp (y, target.position.y + distFromTargetY, smoothing * Time.deltaTime);
				else y = target.position.y + distFromTargetY;
				//y = target.position.y + distFromTargetY;
			}
			float cameraHalfWidth = camera.orthographicSize * ((float) Screen.width / Screen.height);

			x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
			y = Mathf.Clamp(y, min.y + camera.orthographicSize, max.y - camera.orthographicSize);
			transform.position = new Vector3 (x, y, transform.position.z);
		}
	}
}
