using UnityEngine;
using System.Collections;

public class AnimateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveBy(GameObject.Find("TitleText"),
		              iTween.Hash("x", 5, "y", 5, "easeType", "easeInOutSine", "loopType",
		            "pingpong", "delay", 0.0, "time", 5.0));
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LoadLevel0 () {
		Application.LoadLevel (0);
	}

	public void LoadLevel1 () {
		Application.LoadLevel (1);
	}

	public void LoadLevel2 () {
		Application.LoadLevel (2);
	}
}
