using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockColor : MonoBehaviour {
	private SpriteRenderer sRenderer;
	private FlockBiod fBoid;

	private Color destColor;

	private float colorChangeFrequency = 2f;
	private float timeSinceChange;
	private int colorChangeRarity = 80;

	private float colorAveFrequency = 1f;
	private float timeSinceAve;

	private float colorChangeSpeed = 1;
	private float changeOffset = 0.5f;

	private float minCol = 0.3f;
	private float maxCol = 1f;

	void Start() {
		sRenderer = this.GetComponent<SpriteRenderer>();
		fBoid = this.GetComponent<FlockBiod>();

		destColor = sRenderer.color;

		timeSinceChange = 0;
		timeSinceAve = 0;
	}

	void Update() {
		timeSinceChange += Time.deltaTime;
		if (timeSinceChange >= colorChangeFrequency) {
			ChangeColor();
			timeSinceChange = 0;
		}

		timeSinceAve += Time.deltaTime;
		if (timeSinceAve >= colorAveFrequency) {
			AveWithNearby();
			timeSinceAve = 0;
		}

		sRenderer.color = Color.Lerp(sRenderer.color, destColor, Time.deltaTime * colorChangeSpeed);
	}

	private void ChangeColor() {
		if (Random.Range(1, colorChangeRarity) == 1) {

			Color c = sRenderer.color;

			destColor.r = Mathf.Clamp(Random.Range(c.r - changeOffset, c.r + changeOffset), minCol, maxCol);
			destColor.g = Mathf.Clamp(Random.Range(c.g - changeOffset, c.g + changeOffset), minCol, maxCol);
			destColor.b = Mathf.Clamp(Random.Range(c.b - changeOffset, c.b + changeOffset), minCol, maxCol);

			SetNearbyDestColor(destColor);
		}
	}

	private void SetNearbyDestColor(Color c) {
		GameObject[] nearbyBoids = fBoid.NearbyBoids;
		for (int i = 0; i < nearbyBoids.Length; i++) {
			nearbyBoids[i].GetComponent<FlockColor>().destColor = c;
		}
	}

	private void AveWithNearby() {
		Color totalColor = Color.black;

		GameObject[] nearbyBoids = fBoid.NearbyBoids;
		for (int i = 0; i < nearbyBoids.Length; i++) {
			totalColor += nearbyBoids[i].GetComponent<FlockColor>().destColor;
		}

		Color aveColor = totalColor / (nearbyBoids.Length);
		destColor = aveColor;
	}
}
