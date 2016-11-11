using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleMotion : MonoBehaviour {

	Text text;
	public float amplitude;
 
    protected void Start() {
		text = GetComponent<Text>();
    }
 
    protected void Update() {
        float value = amplitude * Mathf.Sin(Time.timeSinceLevelLoad);
		text.lineSpacing = value; 
    }
}
