using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {

	public GameController gc;
	public AudioClip chime;
	public AudioSource audioSource;
	[SerializeField] private float speed;
	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = chime;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider collider) {
		if(collider.CompareTag("Player")) {
			//decrement gem count
			StartCoroutine(CollectGemAndDestroy(collider.gameObject));
		}
	}

	public IEnumerator CollectGemAndDestroy(GameObject obj) {
		//Assumes the player is the only other collider
		//TODO suck gem into player?
		//Play sound effect?
		//destroy the gem
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;

		gc.gemsRemaining--;
		audioSource.PlayOneShot(chime);
		while(audioSource.isPlaying) {
			yield return null;
		}

		GameObject.Destroy(this.gameObject);
	}
}
