using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {
	public Camera cam;

	private float rotationSpeed = 1;

	void Update() {
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;

		transform.position = Vector3.Lerp(transform.position, mousePos, Time.deltaTime * 5);
	}
}
