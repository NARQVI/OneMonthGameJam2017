using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatterOnCollision : MonoBehaviour {
    public GameObject replacement;
	public float timeRemove = 2f;
	private string objectAnforaSound = "event:/Objects/Anforas"; // Refence to  FMOD event

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.CompareTag ("PlayerWeapon")) {
			var shatterReplacement = GameObject.Instantiate (replacement, transform.position + Vector3.up * 1, transform.rotation, transform.parent.transform);
			Destroy (gameObject);
			Destroy (shatterReplacement, timeRemove);
			DestroySound ();

		}
    }

	public void DestroySound()
	{
		
		FMODUnity.RuntimeManager.PlayOneShot(objectAnforaSound); // Play One Shot when Attack state is activated

	}

}
