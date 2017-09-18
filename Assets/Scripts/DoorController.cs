using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public int cantEnemies;

	Animator anim;
	Collider[] colls;
	int deadEnemies=0;


	void Start()
	{
		anim = GetComponent<Animator> ();
		colls =GetComponents<Collider> () as Collider[];
	}

	void Update()
	{
		if (deadEnemies == cantEnemies) {
			anim.SetBool ("OpenL",true);
			anim.SetBool ("OpenR",true);
			deadEnemies = 0;
			colls[1].enabled = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player")) {
			anim.SetBool ("OpenL",false);
			anim.SetBool ("OpenR",false);
			colls[1].enabled = true;
		}
	}



	public void CountEnemy()
	{

		deadEnemies++;
	}
}
