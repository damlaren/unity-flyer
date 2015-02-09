using UnityEngine;
using System.Collections;

public class SupportScript : MonoBehaviour {

	public int hp = 50;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision collision) {
		// reduce hp on collision with a bullet
		GameObject g = collision.gameObject;
		if (g.GetComponent<BulletScript> () != null) {
			hp--;
			if (hp <= 0) {
				Destroy(gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
