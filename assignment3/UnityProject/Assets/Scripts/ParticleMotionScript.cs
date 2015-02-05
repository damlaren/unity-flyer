using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class ParticleMotionScript : MonoBehaviour {
	float[] particle_states;
	float[] goal_position;
	float[] weights;

	[DllImport("Assets/Plugins/ParticlePlugin")]
	private static extern void flock(float[] particle_states, float[] goal_position,
	                                 float[] weights, float dt);

	// Use this for initialization
	void Start () {
		particle_states = new float[6];
		goal_position = new float[3] {25, 8, 0};
		weights = new float[5] {100, 1, 1, 1, 1};

		////position
		for(int d=0; d < 3;d++) particle_states[d] = transform.position[d];
		////velocity
		for(int d=0; d < 3;d++) particle_states[d + 3]=0;
	}
	
	void FixedUpdate () {
		float dt = Time.deltaTime;
		flock (particle_states, goal_position, weights, dt);
		transform.position = new Vector3 (particle_states[0], particle_states[1],
		                                  particle_states[2]);
	}
}
