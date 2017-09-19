using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	Image bkgImage;
	GameObject pauseText;
	bool isPaused;
	bool isQuit;

	//Referencias al control de la música
	private GameObject musicSystemObject;
	public MusicController musicSystem;

	// Use this for initialization
	void Start () {
		SetUp ();

		//Referencias al control de la música
		musicSystemObject = GameObject.Find ("MusicManager");
		musicSystem = musicSystemObject.GetComponent<MusicController>();
	}
	
	// Update is called once per frame
	void Update () {

		if (isQuit) {
			if(SceneManager.GetActiveScene().name=="Lvl1")
				SetUp ();
		}

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			isPaused = !isPaused;
			Pause ();
		}

		if (isPaused && Input.GetKeyDown (KeyCode.Q)) {
			isPaused = false;
			Pause ();
			isQuit = true;
			SceneManager.LoadScene (0);
			musicSystem.BackToMusic ();
		}
	}


	void SetUp()
	{
		bkgImage = GameObject.Find ("PauseBgImage").GetComponent<Image>() ;
		pauseText = GameObject.Find ("PauseText");
		bkgImage.enabled = false;
		pauseText.SetActive (false);
		isPaused = false;
		isQuit = false;
	}

	void Pause()
	{
		Debug.Log (isPaused);
		bkgImage.enabled = isPaused;
		pauseText.SetActive (isPaused);
		Time.timeScale = isPaused ? 0 : 1;
		musicSystem.MenuMusic ();
	}
		
}
