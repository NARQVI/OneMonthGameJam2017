using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffectController : MonoBehaviour {

	ParticleSystem pSystem;

	void Start() 
	{
		pSystem = GetComponent<ParticleSystem> ();
		var pSystemEmission = pSystem.emission;	//Guarda el modulo de emisión
		pSystemEmission.enabled = false;	//Desactiva la emisión

	
	}
		

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) {
			var pSystemEmission = pSystem.emission;	//Guarda el modulo de emisión
			pSystemEmission.enabled = true;	//Activa la emisión
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
			var pSystemEmission = pSystem.emission;	//Guarda el modulo de emisión
			pSystemEmission.enabled = false;	//Activa la emisión
		}
	}
		
}
