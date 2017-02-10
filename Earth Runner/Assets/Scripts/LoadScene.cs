using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public void loadByIndex(int i) {
		SceneManager.LoadScene (i);
	}
}
