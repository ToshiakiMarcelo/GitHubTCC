using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{
	public  Vector3   startPos;
	private  Camera    cam;
	public  Vector3   offset;
	public  float     offsetXValue;
	public  float     offsetYValue;
	private Transform cameraInitPos;
	private Transform cameraPos;

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

		transform.localPosition = new Vector3 ((cameraPos.localPosition.x * offsetXValue) + offset.x,
											   (cameraPos.localPosition.y * offsetYValue) + offset.y,
												startPos.z);
	}
}
