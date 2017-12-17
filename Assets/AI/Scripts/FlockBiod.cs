using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBiod : MonoBehaviour {
	private float nearbyRadius = 3;
	private float tooCloseRadius = 0.8f;
	private GameObject[] nearbyBoids;

	private float boidDetectFrequency = 1;
	private float timeSinceDetect;

	private float rotationSpeed = 1;
	private float moveSpeed = 2;

	private float separationWeight = 0.8f;
	private float separationWeightClose = 1.2f;

	void Start() {
		timeSinceDetect = 0;
		DetectNearbyBoids();
	}

	void Update() {
		timeSinceDetect += Time.deltaTime;
		if (timeSinceDetect >= boidDetectFrequency) {
			DetectNearbyBoids();
			timeSinceDetect = 0;
		}

		Flock();
	}

	private void DetectNearbyBoids() {
		Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, nearbyRadius);
		nearbyBoids = new GameObject[colls.Length];
		for (int i = 0; i < colls.Length; i++) {
			nearbyBoids[i] = colls[i].gameObject;
		}
	}

	private void Flock() {
		float totalRotation = 0;
		Vector3 totalPos = Vector3.zero;
		Vector3 totalDist = Vector3.zero;

		float tooCloseBoids = 0;
		Vector3 totalDistClose = Vector3.zero;

		for (int i = 0; i < nearbyBoids.Length; i++) {
			totalRotation += nearbyBoids[i].transform.rotation.eulerAngles.z;
			totalPos += nearbyBoids[i].transform.position;

			Vector3 dist = (nearbyBoids[i].transform.position - this.transform.position);

			totalDist += dist;

			if (dist.magnitude <= tooCloseRadius) {
				tooCloseBoids++;
				totalDistClose += dist;
			}
		}

		// Alignment
		float aveRotation = totalRotation / nearbyBoids.Length;
		Quaternion newRotation = Quaternion.Euler(0, 0, aveRotation);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

		// Cohesion
		Vector3 avePos = totalPos / nearbyBoids.Length;
		// transform.position = Vector3.Lerp(transform.position, avePos, Time.deltaTime * moveSpeed);

		// Mid-range repulsion
		Vector3 aveDist = totalDist / nearbyBoids.Length;
		// transform.position = Vector3.Lerp(transform.position, transform.position - (aveDist * separationWeight), Time.deltaTime * moveSpeed);

		// Close-range repulsion
		Vector3 aveDistClose = totalDistClose / tooCloseBoids;
		// transform.position = Vector3.Lerp(transform.position, transform.position - (aveDistClose * separationWeightClose), Time.deltaTime * moveSpeed);

		Vector3 moveVec = avePos - (aveDist * separationWeight) - (aveDistClose * separationWeightClose);
		transform.position = Vector3.Lerp(transform.position, moveVec, Time.deltaTime * moveSpeed);
	}

	/*
	private void AlignmentWithNearbyBoids() {
		// Alignment
		float totalRotation = 0;
		for (int i = 0; i < nearbyBoids.Length; i++) {
			totalRotation += nearbyBoids[i].transform.rotation.z;
		}
		float aveRotation = totalRotation / nearbyBoids.Length;

		Quaternion newRotation = transform.rotation;
		newRotation.z = aveRotation;
		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation
			, Time.deltaTime * rotationSpeed);
	}

	private void CohesionWithNearbyBoids() {
		// Cohesion
		Vector3 totalPos = Vector3.zero;
		for (int i = 0; i < nearbyBoids.Length; i++) {
			totalPos += nearbyBoids[i].transform.position;
		}
		Vector3 avePos = totalPos / nearbyBoids.Length;

		transform.position = Vector3.Lerp(transform.position, avePos
			, Time.deltaTime * moveSpeed);
	}

	private void SeparationFromNearbyBoids() {
		// Separation
		Vector3 totalDist = Vector3.zero;
		for (int i = 0; i < nearbyBoids.Length; i++) {
			totalDist += (nearbyBoids[i].transform.position - this.transform.position);
		}
		Vector3 aveDist = totalDist / nearbyBoids.Length;

		transform.position = Vector3.Lerp(transform.position, transform.position - (aveDist * separationWeight)
			, Time.deltaTime * moveSpeed);
	}
	*/
}

// https://gamedevelopment.tutsplus.com/tutorials/3-simple-rules-of-flocking-behaviors-alignment-cohesion-and-separation--gamedev-3444
// https://en.wikipedia.org/wiki/Flocking_%28behavior%29#Flocking_rules
