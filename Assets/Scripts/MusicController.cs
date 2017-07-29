using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {


	public static MusicController instance = null;

	[FMODUnity.EventRef]
	public string music = "event:/Music/Music"; // Ref al evento de la música

	FMOD.Studio.EventInstance musicEv; // Instancia del evento de la música


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

	public void mundo(){

		musicEv.setParameterValue ("Mundo", 1f);
	}

	public void lvl2(){

		musicEv.setParameterValue ("lvl2", 1f);
	}

	// Update is called once per frame
	void Update () {


	}
}
