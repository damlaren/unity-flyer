#define EXPORT_API __declspec(dllexport) // Visual Studio needs to annotate exported functions with this
#include <cmath>
#include <cstdlib>
#include <string.h>

extern "C"
{
	/**
	  * Store normalized vector vin with n entries into vout.
	  */
	void normalize(float *vout, float* vin, int n) {
		float s = 0;
		for (int d = 0; d < n; d++) {
			s += vin[d] * vin[d];
		}
		float mag = std::sqrt(s);
		if (mag != 0) {
			for (int d = 0; d < n; d++) {
				vout[d] = vin[d] / mag;
			}
		}
	}

	/** Multiply vector v of length n by scalar s, store into vout. */
	void multiply(float* vout, float* vin, float s, int n) {
		for (int d = 0; d < n; d++) {
			vout[d] = vin[d] * s;
		}
	}

	/** Store vin + win in vout. v, w must be length n. */
	void add(float *vout, float *vin, float *win, int n) {
		for (int d = 0; d < n; d++) {
			vout[d] = vin[d] + win[d];
		}
	}

	/** Store vin + win in vout. v, w must be length n. */
	void subtract(float *vout, float *vin, float *win, int n) {
		float wCopy[3];
		multiply(wCopy, win, -1.0f, n);
		add(vout, vin, wCopy, n);
	}

	/** Copy vector vin of length n to vout. */
	void copy(float* vout, const float* vin, int n) {
		memcpy(vout, vin, n * sizeof(float));
	}

	/**
	  * Model a flock of birds.
	  * @particle_states: [x; v] for this particle.
	  * @goal_position: [x] goal position.
	  * @param neighbor_positions: positions of nearest neighbors
	  * @param neighbor_directions: directions of nearest neighbors
	  * @weights: 6 of them, respectively for: desired motion, separation, alignment, cohesion, avoidance, randomness
	  * @dt: timestep
	  */
    const EXPORT_API void flock(float* particle_states, float* goal_position,
		float* neighbor_positions, float* neighbor_directions, int num_neighbors,
		float* weights, float dt)
    {
        float* x = &particle_states[0];
        float* v = &particle_states[3];

		// Force 1: Desired motion
		float currentDirection[3];
		float desiredDirection[3];
		float desiredDirectionDifference[3];
		float desiredDirectionForce[3];
		float desiredDirectionWeight = weights[0];
		subtract(desiredDirection, goal_position, x, 3);
		normalize(currentDirection, v, 3);
		normalize(desiredDirection, desiredDirection, 3);
		subtract(desiredDirectionDifference, desiredDirection, currentDirection, 3);
		multiply(desiredDirectionForce, desiredDirectionDifference, desiredDirectionWeight, 3);

		// Forces 2, 3, 4: Cohesion, Alignment, Separation
		for (int neighbor_index = 0; neighbor_index < num_neighbors; neighbor_index++) {
			int value_index = neighbor_index * 3;

			float* neighbor_position = neighbor_positions + value_index;
			float* neighbor_direction = neighbor_directions + value_index;
		}

		// Force 5: Avoidance is ignored

		// Force 6: Random variation
		float randomForce[3];
		float randomWeight = weights[5];
		for (int d = 0; d < 3; d++) {
			randomForce[d] = (static_cast<float>(rand() % 1000) / 1000.0f - 0.5f) * randomWeight;
		}

		// Add all the forces together at the end, transform to impulse.
		float totalImpulse[3];
		add(totalImpulse, totalImpulse, desiredDirectionForce, 3);
		add(totalImpulse, totalImpulse, randomForce, 3);
		multiply(totalImpulse, totalImpulse, dt, 3);

		// Update velocity and position.
		float posChange[3];
		add(v, v, totalImpulse, 3);
		multiply(posChange, v, dt, 3);
		add(x, x, posChange, 3);
    }
    
} // end of export C block
