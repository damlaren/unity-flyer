using UnityEngine;
using System.Collections;

public class AnimateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//iTween.MoveBy(GameObject.Find("TitleText"),
		 //             iTween.Hash("y", 5, "easeType", "easeInOutSine", "loopType",
		  //          			  "pingpong", "delay", 0.0, "time", 4.0));
		iTween.MoveBy(GameObject.Find("TitleText"),
		              iTween.Hash("x", 5, "y", 5, "easeType", "easeInOutSine", "loopType",
		            "pingpong", "delay", 0.0, "time", 5.0));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
