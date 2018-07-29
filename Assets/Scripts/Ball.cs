using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball: MonoBehaviour {
	private bool isCaptured = true;
	private Rigidbody2D rb;
	private static readonly float basePush = 40.0f;
	private readonly Vector2 initialPush = new Vector2(basePush, basePush * Mathf.Sqrt(3.0f));

	// Awake is called when the script instance is being loaded
	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	private void Start() {
		rb.bodyType = RigidbodyType2D.Kinematic;
	}

	// Update is called once per frame
	private void Update() {
		if(Input.GetButtonDown("Fire1") && isCaptured) {
			isCaptured = false;
			transform.parent = null;
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.AddForce(initialPush);
			GameManager.instance.balls.Add(gameObject);
		}
	}
}
