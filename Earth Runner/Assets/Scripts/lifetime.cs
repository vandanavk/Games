using UnityEngine;
using System.Collections;

public class lifetime : MonoBehaviour {

	void Start()
	{
		Invoke("DestroyObject", LifeTime);
	}


	void DestroyObject()
	{
		Destroy(gameObject);
	}


	public float LifeTime = 10f;
}
