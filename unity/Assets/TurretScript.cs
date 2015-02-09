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
		Vector3 wayPoint = target.transform.position;
		Vector3 targetDir = wayPoint - transform.position;
		targetDir.Normalize ();
		Quaternion qTo = Quaternion.LookRotation (targetDir, transform.up);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
		//transform.rotation = Quaternion.Lerp (transform.rotation, qTo, Time.deltaTime * rotationSpeed);
		if ((wayPoint - transform.position).magnitude < 500) {
			Instantiate (bulletPrefab, transform.position + targetDir * 10.0f, transform.rotation);
		}
	}
}
