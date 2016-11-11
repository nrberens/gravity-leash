using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public int totalGems;
	public int gemsRemaining;
	public Vector3 playerInitPos;

	void Start() {
		gemsRemaining = totalGems;
	}

	void Update() {
		if(gemsRemaining <= 0) {
			EndGame();
		}

		if(Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene("caves");
		}

		if(Input.GetKeyDown(KeyCode.Escape)) {
			//SceneManager.LoadScene("title");
		}
	}

	void EndGame() {
		SceneManager.LoadScene("ending");
	}

}
