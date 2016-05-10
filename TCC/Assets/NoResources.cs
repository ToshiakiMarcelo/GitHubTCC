using UnityEngine;
using System.Collections;

public class NoResources : MonoBehaviour {

	void Start () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Resource") {
			Destroy (other.gameObject);
		}
	}
}
