using UnityEngine;
using System.Collections;

public class CameraControllerV2 : MonoBehaviour {

	private Vector3 min;
	private Vector3 max;
	public  Transform target;
	public  Transform finalDaFase;
	private float distX;
	private float distY;
	private float distX2;
	private float distY2;
	public  float offsetX;
	public  float offsetY;
	public  bool canSmooth;
	//public  Vector2 margin;
	public  float smoothing;
	public  float distanceToSmoothing = 3;
	public  BoxCollider2D cameraLimit;
	private Camera camera;
	private float distFromXOG;
	public  bool IsFollowing {get; set;}


	public void Start() {
		min = cameraLimit.bounds.min;
		max = cameraLimit.bounds.max;
		IsFollowing = target != null;

		distFromXOG = offsetX;
		camera = GetComponent<Camera>();
	}

	public void LateUpdate() {
		float x = transform.position.x;
		float y = transform.position.y;

		distX = finalDaFase.position.x - target.position.x;
		distY = finalDaFase.position.y - target.position.y;

		distX2 = ((distX/10 - 7.5f)* -1);
		distY2 = Mathf.Lerp(0, (distY/10), distX2/6);

		offsetX = Mathf.Clamp(distX2, 0, 6f);
		offsetY = Mathf.Clamp(distY2, -1f, 1);

		camera.orthographicSize = Mathf.Clamp( (((distX*-0.1f) + 8)), 5.5f, 6);

		if (IsFollowing) {
			if (Mathf.Abs(x - target.position.x) > 0) {
				if (Mathf.Abs((x - target.position.x) - offsetX) > distanceToSmoothing) x = Mathf.Lerp (x, target.position.x + offsetX, smoothing * Time.deltaTime);
				else x = target.position.x + offsetX;
			}

			if (Mathf.Abs (y - target.position.y) > 0) {
				if (Mathf.Abs ((y - target.position.y) - offsetY) > distanceToSmoothing) y = Mathf.Lerp (y, target.position.y + offsetY, smoothing * Time.deltaTime);
				else y = target.position.y + offsetY;
			}
			float cameraHalfWidth = camera.orthographicSize * ((float) Screen.width / Screen.height);

			x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
			y = Mathf.Clamp(y, min.y + camera.orthographicSize, max.y - camera.orthographicSize);
			transform.position = new Vector3 (x, y, transform.position.z);
		}
	}
}