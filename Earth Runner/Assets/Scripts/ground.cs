using UnityEngine;
using System.Collections;

public class ground : MonoBehaviour {

	public float positionZ = 20f;
	public Transform[] PathSpawnPoints;
	public GameObject Ground;

	void OnTriggerEnter(Collider hit)
	{
		//player has hit the collider
		if (hit.gameObject.CompareTag ("Player"))
		{
			int randomSpawnPoint = Random.Range(0, PathSpawnPoints.Length);
			for (int i = 0; i < PathSpawnPoints.Length; i++)
			{
				Vector3 target = PathSpawnPoints [i].position;
				target.z += positionZ;

				if (i == randomSpawnPoint)
					Instantiate (Ground, target, PathSpawnPoints [i].rotation);

			}

		}
	}

}
