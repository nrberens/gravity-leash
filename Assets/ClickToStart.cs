using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickToStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)) {
			StartGame();
		}

		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void StartGame() {
		SceneManager.LoadScene("caves");
	}
}
