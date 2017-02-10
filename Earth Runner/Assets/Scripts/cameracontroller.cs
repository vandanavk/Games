using UnityEngine;
using System.Collections;

public class cameracontroller : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//offset = new Vector3(0.0f, player.GetComponent<player>().size / 2 + 3, -player.GetComponent<player>().size / 2- 5);
		transform.position = player.transform.position + offset;
	}
}
