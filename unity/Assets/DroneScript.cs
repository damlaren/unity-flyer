using UnityEngine;
using System.Collections;

public class DroneScript : MonoBehaviour {

	float speed = 70.0f;
	float rotationSpeed = 25.0f;
	Vector3 wayPoint;
	Vector3 initialPosition;
	int killCount = 0;
	public GameObject target = null;

	void GenerateWayPoint() {
		float range = 800.0f;
		wayPoint = new Vector3 (Random.Range (-range, range),
		                        Random.Range (150.0f, 200.0f),
		                        Random.Range (-range, range));
	}

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		Restart ();
	}

	void Restart() {
		transform.position = initialPosition;
		GenerateWayPoint ();
	}

	void OnCollisionEnter(Collision collision)
	{
		killCount++;
		print ("yeah! get those russians!" + killCount);
		Restart ();
	}
	
	// Update is called once per frame
	void Update () {

		// Check if waypoint is close enough
		if (target == null) {
			if ((transform.position - wayPoint).magnitude < 20.0f) {
				GenerateWayPoint ();
			}
		} else {
			wayPoint = target.transform.position;
		}

		// turn towards waypoint
		transform.rotation = Quaternion.RotateTowards (transform.rotation,
		                                               Quaternion.LookRotation (wayPoint - transform.position),
		                                               rotationSpeed * Time.deltaTime);

		// forward motion speed
		float translation = speed * Time.deltaTime;

		// Randomize motion a little bit
		float maxPerturb = 1.0f * Time.deltaTime;
		float perturbX = Random.Range (-maxPerturb, maxPerturb);
		float perturbY = Random.Range (-maxPerturb, maxPerturb);
		float perturbZ = Random.Range (-maxPerturb, maxPerturb);
		
		// Translate along object's new forward axis.
		transform.position = transform.position + transform.forward * translation +
			new Vector3 (perturbX, perturbY, perturbZ);
	}
}
