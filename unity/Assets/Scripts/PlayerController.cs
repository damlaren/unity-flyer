using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	float minSpeed = 80.0f;
	float speed = 0.0f;
	float maxSpeed = 80.0f;
	float acceleration = 20.0f;
	float rotationSpeed = 60.0f; // Roll rate and pitch rate

	public GameObject bulletPrefab; // Assign this prefab in the editor

	void OnCollisionEnter(Collision collision)
	{
		print ("you are dead");
		minSpeed = 0;
		maxSpeed = 0;
		speed = 0;
	}

	void Update()
	{
		float da = acceleration * Time.deltaTime;
		if (Input.GetKey (KeyCode.F)) {
			speed += da;
		}
		else if (Input.GetKey (KeyCode.V)) {
			speed -= da;
		}
		if (speed > maxSpeed) {
			speed = maxSpeed;
		}
		else if (speed < minSpeed) {
			speed = minSpeed;
		}
		
		// shoot dem bullets
		if (Input.GetKey (KeyCode.Space)) {
			Instantiate (bulletPrefab, transform.position + transform.forward * 10.0f, transform.rotation);
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
