using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class quit_app : MonoBehaviour {

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	public void doExitGame() {
		Application.Quit();
	}
}
