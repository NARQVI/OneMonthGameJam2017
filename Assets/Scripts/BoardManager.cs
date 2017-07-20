using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public GameObject basicLayout; // Escenario básico


	//Carga la escena dependiendo del nivel
	public void SetupScene(int level)
	{
		Instantiate (basicLayout,basicLayout.transform.position,Quaternion.identity);
	}
}
