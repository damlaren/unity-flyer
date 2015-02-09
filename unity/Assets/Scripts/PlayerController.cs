﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	float speed = 30.0f; // There is only one speed: fast.
	float rotationSpeed = 60.0f; // Roll rate and pitch rate

	void OnCollisionEnter(Collision collision)
	{
		print ("you are dead");
		speed = 0;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (speed == 0) {
				speed = 30.0f;
			}
			else {
				speed = 0;
			}
		}
		// Get the horizontal and vertical axis.
		// By default they are mapped to the arrow keys.
		// The value is in the range -1 to 1
		float pitch = Input.GetAxis ("Vertical") * rotationSpeed;
		float roll = Input.GetAxis ("Horizontal") * rotationSpeed;

		// Make it move 10 meters per second instead of 10 meters per frame...
		pitch *= Time.deltaTime;
		roll *= Time.deltaTime;
		float translation = speed * Time.deltaTime;

		// Move translation along the object's z-axis
		transform.Translate (0, 0, translation);

		// Roll about z-axis
		transform.Rotate (0, 0, -roll);

		// Pitch about x-axis
		transform.Rotate (pitch, 0, 0);
	}
}
