using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) {
			anim.SetBool ("OpenR",true);
			anim.SetBool ("OpenL",true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player")) {
			anim.SetBool ("OpenR",false);
			anim.SetBool ("OpenL",false);
		}
	}
}
