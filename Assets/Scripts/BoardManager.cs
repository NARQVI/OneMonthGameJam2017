using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public GameObject basicLayout; // Escenario básico

	public GameObject spikes;
	public GameObject towers;
	public GameObject healStatue;
	public GameObject anfora;
    public GameObject enemy;
    public GameObject civilian;
    //Carga la escena dependiendo del nivel
    public void SetupScene(int level)
	{
		var basicLayoutIns=Instantiate (basicLayout,basicLayout.transform.position,Quaternion.identity);

		//Carga los objetos para el nivel 1
		if (level == 1)
			Instantiate (anfora,basicLayoutIns.transform,false);

		//Carga los objetos para el nivel 2
		if (level == 2) {
			Instantiate (spikes,basicLayoutIns.transform,false);
			Instantiate (towers,new Vector3(2.3f,-3.6f,0f),Quaternion.identity,basicLayoutIns.transform);
			Instantiate (healStatue,basicLayoutIns.transform,false);
		}
        if (level == 3)
        {
            Instantiate(civilian, new Vector3(5f, -3f, 3f), Quaternion.identity, basicLayoutIns.transform);
            Instantiate(enemy, new Vector3(2.3f, 0, 0f), Quaternion.identity, basicLayoutIns.transform);
           
        }
    }
		
}
