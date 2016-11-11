using UnityEngine;
using System.Collections;

public class TitleCameraRotate : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
 void Update () {
	 	transform.RotateAround(transform.position, transform.up, speed * Time.deltaTime);
	}
}
