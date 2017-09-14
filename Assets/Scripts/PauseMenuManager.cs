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

	// Use this for initialization
	void Start () {
		SetUp ();
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
	}
		
}
