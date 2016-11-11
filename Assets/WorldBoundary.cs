using UnityEngine;
using System.Collections;

public class WorldBoundary : MonoBehaviour {

	GameObject player;
	AudioSource audioSource; 
	GameController gc;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	void Update() {
		if(player.transform.position.y <= -500f) {
			player.transform.position = gc.playerInitPos;
		}
	}
}
