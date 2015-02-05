using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class RigidBodyMotionScript : MonoBehaviour {
	private float[] rigid_body_states;
    private IntPtr rigid_body = IntPtr.Zero;

	[DllImport ("RigidBodyPlugin")]
	private static extern void advanceCube(ref IntPtr rigid_body,float[] rigid_body_states,float dt);

	[DllImport ("RigidBodyPlugin")]
	private static extern void createCube (ref IntPtr rigid_body,float[] rigid_body_states);

	[DllImport ("RigidBodyPlugin")]
	private static extern void destroyCube (ref IntPtr rigid_body);
	
	// Use this for initialization
	void Start () {
		rigid_body_states = new float[7];
		for(int d=0;d<3;d++) rigid_body_states[d]=transform.position[d];

		rigid_body_states [3] = transform.rotation.x;
		rigid_body_states [4] = transform.rotation.y;
		rigid_body_states [5] = transform.rotation.z;
		rigid_body_states [6] = transform.rotation.w;

		Debug.Log ("init "+rigid_body_states [3]+", "+rigid_body_states [4]+", "+rigid_body_states [5]+", "+rigid_body_states [6]);

		createCube(ref rigid_body, rigid_body_states);
	}
	
	void FixedUpdate () {
		float dt = Time.deltaTime;
		advanceCube (ref rigid_body, rigid_body_states, dt);
		transform.position = new Vector3 (rigid_body_states[0],rigid_body_states[1],rigid_body_states[2]);
		transform.rotation = new Quaternion (rigid_body_states [3],rigid_body_states [4],rigid_body_states [5],rigid_body_states [6]);
		//Debug.Log (rigid_body_states [3]+", "+rigid_body_states [4]+", "+rigid_body_states [5]+", "+rigid_body_states [6]);
	}

	void OnDestroy (){
		destroyCube (ref rigid_body);
	}
}
