using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel(0);
		}
		else if (Input.GetKeyDown (KeyCode.C)) {
			Camera projCamera = GameObject.Find ("Main Camera").camera;
			Camera orthoCamera = GameObject.Find ("Ortho Camera").camera;
			projCamera.enabled = !projCamera.enabled;
			orthoCamera.enabled = !orthoCamera.enabled;
		}
	}
}
