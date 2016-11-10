using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIArtifactIndicator : MonoBehaviour {

	public Text text;
	public GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Artifacts Remaining: " + gc.gemsRemaining;
	
	}
}
