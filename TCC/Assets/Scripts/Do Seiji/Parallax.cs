using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{
	public  Vector3   startPos;
	private Camera    cam;
	public  Vector3   offset;
	public  float     offsetXValue;
	public  float     offsetYValue;
	private Transform cameraInitPos;
	private Transform cameraPos;
	public  BoxCollider2D cameraLimit;
	private Vector3 min;
	private Vector3 max;
	public  bool itMoves;
	public  float bonus;
	public  float speed;

	void Start ()
	{
		cam = Camera.main;
		startPos = transform.localPosition;
		cameraInitPos = cam.transform;
		offset.x = startPos.x - cameraInitPos.localPosition.x * offsetXValue;
		offset.y = startPos.y - cameraInitPos.localPosition.y * offsetYValue;
	}

	void Update ()
	{
		cameraPos = cam.transform;

		if (!itMoves)
		{
			transform.localPosition = new Vector3 ((cameraPos.localPosition.x * offsetXValue) + offset.x,
												   (cameraPos.localPosition.y * offsetYValue) + offset.y,
													startPos.z);
		}

		min = cameraLimit.bounds.min;
		max = cameraLimit.bounds.max;

		if (itMoves)
		{
			transform.Translate(speed * .1f, 0, 0);
			if (transform.position.x > max.x + bonus && speed > 0) transform.position = new Vector3(min.x - bonus, transform.position.y, transform.position.z);
			if (transform.position.x < min.x - bonus && speed < 0) transform.position = new Vector3(max.x + bonus, transform.position.y, transform.position.z);
		}
	}
}
