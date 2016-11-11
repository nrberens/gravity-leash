using UnityEngine;
using System.Collections;

public class GravityLeash : MonoBehaviour {

	public float leashForce;
	public Vector3 point;
	public bool attached;
	public Rigidbody rb;
	public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController rbController;
	public Transform origin;
	public LineRenderer line;
	public Light light;
    public AudioClip beamStart;
    public AudioClip beamLoop;
    public AudioClip beamEnd;
	public AudioClip teleport;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rbController = GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
		origin = transform.Find("LeashOrigin");
		line = origin.GetComponent<LineRenderer>();
		light = GetComponentInChildren<Light>();
        audioSource = GetComponent<AudioSource>();
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
            if(!audioSource.isPlaying)
            {
                audioSource.clip = beamLoop;
                audioSource.Play();
            }
			rb.useGravity = false;
			line.enabled = true;
			light.enabled = true;
			ReelInLeash();
			RenderLeashBeam();
		} else {
            if(audioSource.isPlaying) audioSource.Stop();
			rb.useGravity = true;
			line.enabled = false;
			light.enabled = false;
		}
	}

	void FireLeash() {
        audioSource.PlayOneShot(beamStart);
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

		int layerMask = 1 << 8;
		layerMask = ~layerMask;

		if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
			Debug.DrawRay(transform.position, hit.point-transform.position, Color.blue, 3.0f);
			point = hit.point;
			attached = true;
			rbController.m_leashAttached = true;
		}
	}

	void DetachLeash() {
        audioSource.PlayOneShot(beamEnd);
		attached = false;
		rbController.m_leashAttached = false;
	}

	void ReelInLeash() {
		Vector3 direction = point - transform.position;
		rb.AddForce(direction.normalized * leashForce);	
	}

	void RenderLeashBeam() {
		Vector3 start = origin.position;
		Vector3 end = point;

		Vector3[] positions = new Vector3[] {start, end};
		line.SetPositions(positions);
	}

}

