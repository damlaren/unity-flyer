using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	float speed = 60.0f;
	float distanceTravelled = 0;

	// Use this for initialization
	void Start () {

	}

	void OnCollisionEnter(Collision collision) {
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		distanceTravelled += speed * Time.deltaTime;
		transform.position = transform.position + transform.forward * speed * Time.deltaTime;
		if (distanceTravelled > 300.0f) {
			Destroy (gameObject);
		}
	}
}
