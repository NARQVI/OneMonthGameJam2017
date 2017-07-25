using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {


	public GameObject gameManager; // Prefab de GameManager

	[FMODUnity.EventRef]
	public string music = "event:/Music/Music"; // Ref al evento de la música

	FMOD.Studio.EventInstance musicEv; // Instancia del evento de la música

	// Use this for initialization
	void Start () {
		//gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); //Gets reference

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
