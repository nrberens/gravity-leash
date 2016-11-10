using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {

	public GameController gc;
	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider) {
			//decrement gem count
			gc.gemsRemaining--;
			//TODO suck gem into player?
			//Play sound effect?
			//destroy the gem
			GameObject.Destroy(this.gameObject);
	}
}
