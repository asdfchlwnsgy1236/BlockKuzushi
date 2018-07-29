using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick: MonoBehaviour {
	// OnCollisionEnter2D is called when this collider2D/rigidbody2D has begun touching another rigidbody2D/collider2D (2D physics only)
	private void OnCollisionEnter2D(Collision2D collision) {
		GameManager.instance.BrickBroken();
		Destroy(gameObject);
	}
}
