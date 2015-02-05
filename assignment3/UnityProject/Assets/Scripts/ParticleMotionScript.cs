using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class ParticleMotionScript : MonoBehaviour {
	float[] particle_states;
	float[] center;

	[DllImport("ParticlePlugin")]
	private static extern void centralGravity(float[] particle_states,float[] center,float dt);

	// Use this for initialization
	void Start () {
		particle_states = new float[6];
		center = new float[3];

		////position
		for(int d=0;d<3;d++) particle_states[d]=transform.position[d];
		////velocity
		for(int d=0;d<3;d++) particle_states[d+3]=0;
		////center
		for(int d=0;d<3;d++) center[d]=0;
	}
	
	void FixedUpdate () {
		float dt = Time.deltaTime;
		centralGravity (particle_states,center,dt);
		transform.position = new Vector3 (particle_states[0],particle_states[1],particle_states[2]);
	}
}
