using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Vector3       min;
	private Vector3       max;
	private Transform     trans;
	public  Transform     target;
	public  float         distFromTargetX;
	public  float         distFromTargetY;
	public  Vector2       margin;
	public  Vector2       smoothing;
	public  BoxCollider2D bounds;
	private Camera        camera;
//	public  InputState    playerInputState;
	private float         distFromXOG;
	public  bool          IsFollowing { get; set; }


	public void Start() {
		trans       = transform;
		min         = bounds.bounds.min;
		max         = bounds.bounds.max;
		IsFollowing = target != null;

		distFromXOG = distFromTargetX;

		camera = GetComponent<Camera>();
	}

	public void Update()
	{
//		if (playerInputState.velX > 0)
//		{
//			distFromTargetX = distFromXOG;
//		}
//		if (playerInputState.velX < 0)
//		{
//			distFromTargetX = - distFromXOG;
//		}
	}

	public void LateUpdate() {
		float x = trans.position.x;
		float y = trans.position.y;

		if (IsFollowing) {
			if (Mathf.Abs(x - target.position.x) > margin.x) {
				x = Mathf.Lerp(x, target.position.x + distFromTargetX, smoothing.x * Time.deltaTime);
			}

			if (Mathf.Abs(y - target.position.y) > margin.y) {
				y = Mathf.Lerp(y, target.position.y + distFromTargetY, smoothing.y * Time.deltaTime);
			}

			float cameraHalfWidth = camera.orthographicSize * ((float) Screen.width / Screen.height);

			x              = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
			y              = Mathf.Clamp(y, min.y + camera.orthographicSize, max.y - camera.orthographicSize);
			trans.position = new Vector3(x, y, trans.position.z);
		}
	}
}
