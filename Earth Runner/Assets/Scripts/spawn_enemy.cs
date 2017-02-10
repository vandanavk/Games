using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class spawn_enemy : MonoBehaviour {

	public float spawnTime = 8f;
	public GameObject enemy;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		InvokeRepeating ("enemy_spawn", 8f, 15f);
	}
	
	// Update is called once per frame
	void enemy_spawn () {
		StartCoroutine ("WaitForAd");
		GameObject[] enemies;
		Vector3 target = new Vector3(transform.position.x,0,transform.position.z);
		enemies = GameObject.FindGameObjectsWithTag ("enemy");
		if (enemies.Length < 1) {
			if (player)
				target = new Vector3 (transform.position.x, 0, UnityEngine.Random.Range (player.transform.position.z+5, player.transform.position.z+10));
			Instantiate (enemy, target, Quaternion.identity);
		} else if (enemies.Length == 1) {
			if (enemies[0].gameObject)
				Destroy (enemies[0].gameObject, 2.0f);
		}
	}

	IEnumerator WaitForAd()
	{
		while (Advertisement.isShowing) {
			CancelInvoke ("enemy_spawn");
			yield return null;
		}

		StartCoroutine ("adddelay");
	}

	IEnumerator adddelay()
	{
		yield return new WaitForSeconds(20f);
	}
}
