using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public int totalGems;
	public int gemsCollected;
	public Vector3 playerInitPos;

	void Start() {
		gemsCollected = 0;
	}

	void Update() {
		if(gemsCollected >= totalGems) {
			EndGame();
		}

		if(Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene("caves");
		}

		if(Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("title");
		}
	}

	void EndGame() {
		SceneManager.LoadScene("ending");
	}

}
