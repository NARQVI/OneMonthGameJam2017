using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {


	public static MusicController instance = null;

	[FMODUnity.EventRef]
	public string music = "event:/Music/Music"; // Ref al evento de la música

	FMOD.Studio.EventInstance musicEv; // Instancia del evento de la música


	float valuelvl2 = -1; //Número para saber a que música volver despues de la pausa
	float valuemundo = -1; //Número para saber a que música volver despues de la pausa


	private void Awake() {


		if (instance == null)	//Revisa que no se haya instanciado el objeto
			instance = this;	//Asigna la instancia

		else if (instance != this)	//Destruye el objeto si está instanciado 
			Destroy (gameObject);	//con otro objeto

		//No se destruye el cargar la escena
		DontDestroyOnLoad (gameObject);


	}

	// Use this for initialization
	void Start () {


		musicEv = FMODUnity.RuntimeManager.CreateInstance (music);


		musicEv.start ();
	}

	public void Boss(){

		musicEv.setParameterValue ("Mundo", 1f);
		musicEv.setParameterValue ("lvl2", 0f);
	}

	public void MainMusic(){

		musicEv.setParameterValue ("lvl2", 1f);
		musicEv.setParameterValue ("Mundo", 0f);
	}

	public void MenuMusic(){



	//	valuelvl2 = musicEv.getParameterValue ("lvl2");
	//	valuemundo = musicEv.getParameterValue ("Mundo");

		musicEv.setParameterValue ("lvl2", 0f);
		musicEv.setParameterValue ("Mundo", 0f);
		//Falta agregar la música del menu de pausa

	}


	//Vuelve a la música al modo normal
	public void BackToMusic(){

		musicEv.setParameterValue ("lvl2", valuelvl2);
		musicEv.setParameterValue ("Mundo", valuemundo);

	}


	// Update is called once per frame
	void Update () {


	}
}
