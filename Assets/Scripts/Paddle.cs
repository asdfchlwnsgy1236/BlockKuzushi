using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle: MonoBehaviour {
	private static readonly float yPos = -5.0f, xLimit = 6.0f;
	private Vector2 position;

	// Update is called once per frame
	private void Update() {
		position = new Vector2(Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -xLimit, xLimit), yPos);
		transform.position = position;
	}
}
