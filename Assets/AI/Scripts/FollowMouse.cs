using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {
	private Camera cam;

	private float maxFollowDist = 10;
	private float rotationSpeed = 4;
	private float moveSpeed = 1;
	private float pullWeight = 0.4f;

	void Start() {
		cam = Camera.main;
	}

	void Update() {
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		Vector3 diff = (mousePos - this.transform.position);

		if (diff.magnitude < maxFollowDist) {
			Vector3 pull = diff * pullWeight;
			transform.position = Vector3.Lerp(transform.position, transform.position + pull, Time.deltaTime * moveSpeed);

			Vector3 diffNormal = diff.normalized;
			float diffAngle = Mathf.Atan2(diffNormal.y, diffNormal.x) * Mathf.Rad2Deg;
			Quaternion newRotation = Quaternion.Euler(0, 0, diffAngle);
			transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
		}
	}
}
