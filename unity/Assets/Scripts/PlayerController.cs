using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	float speed = 20.0f; // There is only one speed: FAST!
	float rotationSpeed = 100.0f; // Roll rate and pitch rate

	void Update()
	{
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
