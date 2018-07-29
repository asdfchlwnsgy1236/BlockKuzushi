using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone: MonoBehaviour {
	// OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Ball") {
			GameManager.instance.BallLost();
			Destroy(collision.gameObject);
		}
	}
}
