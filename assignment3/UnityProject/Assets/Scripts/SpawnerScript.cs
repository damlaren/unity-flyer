using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	public float spawnTime = 3f;		// The amount of time between each spawn.
	public float spawnDelay = 0f;		// The amount of time before spawning starts.
	
	public float spawnCrowTime = 8f;
	public float spawnCrowDelay = 6f;
	
	private GameObject [] object_prefabs;		// Array of prefabs.
	private int number;

	// Use this for initialization
	void Start () {
		object_prefabs = new GameObject[1];
		object_prefabs [0] = Resources.Load<GameObject> ("Prefabs/particle_prefab");
		number = 0;

		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}
	
	void Spawn ()
	{
		if(number>=10)return;

		transform.position = new Vector3(0.0f, 0.0f, 0.0f);

		for(int i=0; i<5; i++){
			float r = 10.0f;
			float x_perturb = Random.Range (-r, r);
			float y_perturb = Random.Range (-r, r);
			float z_perturb = Random.Range (-r, r);
			Vector3 pos = new Vector3(transform.position.x+x_perturb, transform.position.y+y_perturb, transform.position.z+z_perturb);
			Instantiate(object_prefabs[0], pos, transform.rotation);
			number++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}