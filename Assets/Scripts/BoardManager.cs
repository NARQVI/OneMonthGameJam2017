using System;
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

	private GameObject musicSystemObject;
	public MusicController musicSystem;

	void onAwake()
	{
		musicSystemObject = GameObject.Find ("MusicManager");
		musicSystem = musicSystemObject.GetComponent<MusicController>();
	}

    //Carga la escena dependiendo del nivel
    public void SetupScene(int level)
	{
		

		var basicLayoutIns=Instantiate (basicLayout,basicLayout.transform.position,Quaternion.identity);
        //Instantiate(spikes, basicLayoutIns.transform, false);
        //Instantiate(towers, new Vector3(2.3f, -3.6f, 0f), Quaternion.identity, basicLayoutIns.transform);
        //Instantiate(healStatue, basicLayoutIns.transform, false);
        //Carga los objetos para el nivel 1
        if (level == 1)
            { 
            Instantiate(anfora, new Vector3(-2.15f, -3.8f, 6.26f), Quaternion.identity, basicLayoutIns.transform);
            Instantiate(anfora, new Vector3(-2.15f, -3.8f, 0.77f), Quaternion.identity, basicLayoutIns.transform);

        }

        //Carga los objetos para el nivel 2
        if (level == 2) {
			Instantiate (spikes, new Vector3(-6.3f,-3f,3f),Quaternion.identity,basicLayoutIns.transform);
			Instantiate (towers,new Vector3(2.3f,-4.0f,0f),Quaternion.identity,basicLayoutIns.transform);
			Instantiate (healStatue,new Vector3(2.3f,-3f,6f),Quaternion.identity,basicLayoutIns.transform);
			musicSystem.MainMusic (); // Cambia la musica al mundo, los parametros son de prueba
		}
        if (level == 3)
        {
            Instantiate(civilian, new Vector3(5f, -3f, 3f), Quaternion.identity, basicLayoutIns.transform);
            Instantiate(enemy, new Vector3(2.3f, -2.17f, 0f), Quaternion.identity, basicLayoutIns.transform);

           
        }
    }

   
}
