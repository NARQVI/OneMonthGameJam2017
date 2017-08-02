using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatterOnCollision : MonoBehaviour {
    public GameObject replacement;
	public float timeRemove = 2f;

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.CompareTag ("Player")) {
			var shatterReplacement = GameObject.Instantiate (replacement, transform.position + Vector3.up * 1, transform.rotation, transform.parent.transform);
			Destroy (gameObject);
			Destroy (shatterReplacement, timeRemove);
		}
    }

}
