using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public GameObject basicLayout; // Escenario básico

	public GameObject spikes;
	public GameObject towers;
	public GameObject healStatue;

	//Carga la escena dependiendo del nivel
	public void SetupScene(int level)
	{
		var basicLayoutIns=Instantiate (basicLayout,basicLayout.transform.position,Quaternion.identity);

		if (level == 2) {
			Instantiate (spikes,basicLayoutIns.transform,false);
			Instantiate (towers,new Vector3(2.3f,-3.6f,0f),Quaternion.identity,basicLayoutIns.transform);
			Instantiate (healStatue,basicLayoutIns.transform,false);
		}
	}
}
