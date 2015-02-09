using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

	public GameObject target;
	public float rotationSpeed = 30.0f;
	public GameObject bulletPrefab; // Assign this prefab in the editor

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Instantiate (bulletPrefab, transform.position + transform.forward * 10.0f, transform.rotation);
		Vector3 wayPoint = target.transform.position;
		wayPoint.y = transform.position.y;
		Quaternion qTo = Quaternion.LookRotation (wayPoint - transform.position, transform.up);
		//transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, qTo, Time.deltaTime * rotationSpeed);
	}
}
