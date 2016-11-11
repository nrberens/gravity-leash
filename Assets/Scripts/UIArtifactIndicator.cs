using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIArtifactIndicator : MonoBehaviour {

	public Text text;
	public GameController gc;
	public char circle;

	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		text = GetComponent<Text>();
		circle = '\u20dd';
	}
	
	// Update is called once per frame
	void Update () {
		text.text = circle.ToString() + " " + gc.gemsRemaining + "/" + gc.totalGems;
	
	}
}
