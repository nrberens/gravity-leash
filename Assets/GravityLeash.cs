﻿using UnityEngine;
using System.Collections;

public class GravityLeash : MonoBehaviour {

	public float leashForce;
	public Vector3 point;
	public bool attached;
	public Rigidbody rb;
	public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController rbController;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rbController = GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
		attached = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButtonDown("Fire1") && !attached) {
			FireLeash();
		}
		
		if(Input.GetButtonDown("Fire2") && attached) {
			DetachLeash();
		}
	}

	void FixedUpdate() {
		if(attached) {
			rb.useGravity = false;
			ReelInLeash();
		} else {
			rb.useGravity = true;
		}
	}

	void FireLeash() {
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
			Debug.DrawRay(transform.position, hit.point-transform.position, Color.blue, 3.0f);
			point = hit.point;
			attached = true;
			rbController.m_leashAttached = true;
		}
	}

	void DetachLeash() {
		attached = false;
		rbController.m_leashAttached = false;
	}

	void ReelInLeash() {
		Vector3 direction = point - transform.position;
		rb.AddForce(direction * leashForce);	
	}
}
