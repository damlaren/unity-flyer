using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class ParticleMotionScript : MonoBehaviour {
	public float[] particle_states;
	public float[] goal_position;
	public float[] weights;
	float switch_time;

	[DllImport("Assets/Plugins/ParticlePlugin")]
	private static extern void flock(float[] particle_states, float[] goal_position,
	                                 float[] neighbor_positions, float[] neighbor_velocities,
	                                 int num_neighbors, float max_speed, float[] weights, float dt);

	// Use this for initialization
	void Start () {
		particle_states = new float[6];
		goal_position = new float[3] {20, 7, 0};
		switch_time = 0;

		// weights: goal, separation, alignment, cohesion, avoidance, random
		weights = new float[6] {0.5f, 20.0f, 10, 1, 0, 10};

		////position
		for(int d=0; d < 3;d++) particle_states[d] = transform.position[d];
		////velocity
		for(int d=0; d < 3;d++) particle_states[d + 3]=0;
	}

	// Distance comparator function
	int nearerBoid(GameObject b1, GameObject b2) {
		if (b1 == b2) {
			return 0;
		}
		if (b1 == this.gameObject) {
			return 0;
		}

		float d1 = (this.gameObject.transform.position - b1.transform.position).sqrMagnitude;
		float d2 = (this.gameObject.transform.position - b2.transform.position).sqrMagnitude;
		if (d1 < d2) {
			return -1;
		} else if (d1 > d2) {
			return 1;
		} else {
			return 0;
		}
	}
	
	void FixedUpdate () {
		switch_time += Time.deltaTime;
		if (switch_time > 5.0f) {
			if (goal_position[0] == 20) {
				goal_position[0] = 10;
			}
			else {
				goal_position[0] = 20;
			}
			switch_time = 0;
		}

		// Find closest neighbors, ignoring direction.
		int max_closest_neighbors = 4;
		GameObject[] boids = GameObject.FindGameObjectsWithTag ("Boid");
		Array.Sort (boids, nearerBoid);
		int neighbors_visited = 0;
		float[] neighbor_positions = new float[max_closest_neighbors * 3];
		float[] neighbor_velocities = new float[max_closest_neighbors * 3];
		foreach (GameObject boid in boids)
		{
			if (boid == this.gameObject) {
				continue;
			}
			if (neighbors_visited == max_closest_neighbors) {
				break;
			}

			ParticleMotionScript otherScript = boid.GetComponent<ParticleMotionScript>();
			float[] other_particle = otherScript.particle_states;

			int neighbor_index = neighbors_visited * 3;
			neighbor_positions[neighbor_index] = other_particle[0];
			neighbor_positions[neighbor_index + 1] = other_particle[1];
			neighbor_positions[neighbor_index + 2] = other_particle[2];
			neighbor_velocities[neighbor_index] = other_particle[3];
			neighbor_velocities[neighbor_index + 1] = other_particle[4];
			neighbor_velocities[neighbor_index + 2] = other_particle[5];
			neighbors_visited++;
		}

		float dt = Time.deltaTime;
		flock (particle_states, goal_position, neighbor_positions, neighbor_velocities,
		       neighbors_visited, 5.0f, weights, dt);
		transform.position = new Vector3 (particle_states[0], particle_states[1],
		                                  particle_states[2]);
		// TODO update orientation too
	}
}
