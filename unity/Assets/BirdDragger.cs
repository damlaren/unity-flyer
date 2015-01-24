using UnityEngine;
using System.Collections;

public class BirdDragger : MonoBehaviour {

	private Vector3 startPoint;
	private Vector3 dragOffset;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		print ("Mouse down");
		startPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPoint.z);
		dragOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (screenPoint);
	}

	void OnMouseDrag() {
		print ("draggin");
		Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPoint.z);
		transform.position = Camera.main.ScreenToWorldPoint (screenPoint) + dragOffset;
	}
}
