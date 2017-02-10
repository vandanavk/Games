using UnityEngine;
using System.Collections;

public class spawn_food : MonoBehaviour {

	//points where stuff will spawn :)
	public Transform[] StuffSpawnPoints;
	//meat gameobjects
	public GameObject[] Food;
	//obstacle gameobjects
	//public GameObject[] Obstacles;

	public float minX = -2f, maxX = 2f;

	// Use this for initialization
	void Start()
	{
		for (int i = 0; i < StuffSpawnPoints.Length; i++)
		{
			//if (Random.Range(0, 3) == 0)
			{
				Vector3 target = StuffSpawnPoints [i].position;
				target.y = 0;
				CreateObject(target, Food[Random.Range(0, Food.Length)]);
			}
		}


	}

	void CreateObject(Vector3 position, GameObject prefab)
	{
		position += new Vector3(Random.Range(minX, maxX), 0, 0);

		Instantiate(prefab, position, Quaternion.identity);
	}


	/*
	public float food_spawnTime = 1.5f;
	public float bonus_food_spawnTime = 4f;
	public float heart_spawnTime = 6f;
	public float maxPickups = 100;

	public GameObject food_pickup;
	public GameObject bonus_food_pickup;
	public GameObject heart_pickup;

	void Start () {
		int i;
		for (i = 0; i <= maxPickups * (3.0f/4.0f); i++)
		{
			Instantiate(food_pickup);
		}
		for (i = 0; i <= 5 * (3.0f/4.0f); i++)
			Instantiate (bonus_food_pickup);

		InvokeRepeating("Food_Spawn", food_spawnTime, food_spawnTime);
		InvokeRepeating("Bonus_Food_Spawn", bonus_food_spawnTime, bonus_food_spawnTime);
		InvokeRepeating ("Heart_Spawn", heart_spawnTime, heart_spawnTime);
	}

	void Food_Spawn()
	{
		GameObject[] pickups;

		Vector3 target = new Vector3 (UnityEngine.Random.Range (-6.05f, 3.15f), 0, UnityEngine.Random.Range (-4f, 10f));
		pickups = GameObject.FindGameObjectsWithTag("food");

		if (pickups.Length < maxPickups)
		{
			Instantiate(food_pickup, target, Quaternion.identity);
		}

	}

	void Bonus_Food_Spawn()
	{
		GameObject[] bonus_pickups;
		Vector3 target = new Vector3 (UnityEngine.Random.Range (-6.05f, 3.15f), 0, UnityEngine.Random.Range (-4f, 10f));
		bonus_pickups = GameObject.FindGameObjectsWithTag ("bonus_food");
		if (bonus_pickups.Length < 5)
		{
			Instantiate(bonus_food_pickup, target, Quaternion.identity);
		}
	}

	void Heart_Spawn()
	{
		GameObject[] heart_pickups;
		Vector3 target = new Vector3 (UnityEngine.Random.Range (-6.05f, 3.15f), 0, UnityEngine.Random.Range (-4f, 10f));
		heart_pickups = GameObject.FindGameObjectsWithTag ("heart");
		if (heart_pickups.Length < 1)
		{
			Instantiate (heart_pickup, target, Quaternion.identity);
		}
	}*/
}
