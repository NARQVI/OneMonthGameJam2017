using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTile : MonoBehaviour {

	public float force;
	public float maxWaitTime;

	Rigidbody rb;
	float counter;
	bool fall=false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (fall)
			counter += Time.deltaTime;
		if (counter >= maxWaitTime) {
			rb.velocity = Vector3.down * force;
			rb.isKinematic = false;
		}
			
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag ("Player")) {
			fall = true;
		}
	}
}
