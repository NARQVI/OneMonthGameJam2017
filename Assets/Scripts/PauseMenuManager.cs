using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	Image bkgImage;
	GameObject pauseText;
	bool isPaused;

	// Use this for initialization
	void Start () {
		bkgImage = GameObject.Find ("PauseBgImage").GetComponent<Image>() ;
		pauseText = GameObject.Find ("PauseText");
		bkgImage.enabled = false;
		pauseText.SetActive (false);
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			isPaused = !isPaused;
			bkgImage.enabled = isPaused;
			pauseText.SetActive (isPaused);
			Time.timeScale = isPaused ? 0 : 1;
		}

		if (isPaused && Input.GetKeyDown (KeyCode.Q)) {
			SceneManager.LoadScene (0);
			Time.timeScale = 1;
		}
	}
}
