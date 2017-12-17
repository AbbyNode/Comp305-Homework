using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBiod : MonoBehaviour {
	private float nearbyRadius = 3;
	private GameObject[] nearbyBoids;

	private float boidDetectFrequency = 1;
	private float timeSinceDetect;

	private float rotationSpeed = 2;
	private float moveSpeed = 2;

	private float separationWeight = 0.8f;

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

		for (int i = 0; i < nearbyBoids.Length; i++) {
			totalRotation += nearbyBoids[i].transform.rotation.z;
			totalPos += nearbyBoids[i].transform.position;
			totalDist += (nearbyBoids[i].transform.position - this.transform.position);
		}

		float aveRotation = totalRotation / nearbyBoids.Length;
		Quaternion newRotation = transform.rotation;
		newRotation.z = aveRotation;
		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

		Vector3 avePos = totalPos / nearbyBoids.Length;
		transform.position = Vector3.Lerp(transform.position, avePos, Time.deltaTime * moveSpeed);

		Vector3 aveDist = totalDist / nearbyBoids.Length;
		transform.position = Vector3.Lerp(transform.position, transform.position - (aveDist * separationWeight), Time.deltaTime * moveSpeed);
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