﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour {

	bool isScrolling;
	float rotation;
	public float maxYlimit = 540f; //Height of rectangle message
	GameObject scrollTextGO;

	public Text scrollText;
	public Text continueText;
	public float scrollSpeed = 1.0f;


	//Refencias al sistema de sonido 
	public GameObject musicSystemObject; 
	public MusicController musicSystem;
	 
	// Use this for initialization
	void Start () {
		Setup ();

		continueText.enabled = false;

		scrollTextGO = scrollText.gameObject;

		maxYlimit += scrollTextGO.transform.position.y;

		musicSystemObject = GameObject.Find ("MusicManager");
		musicSystem = musicSystemObject.GetComponent<MusicController>();

	}
	
	// Update is called once per frame
	void Update () {

		// If we are scrolling, perform update action
		if (isScrolling)
		{
			// Get the current transform position of the panel
			Vector3 _currentUIPosition = scrollTextGO.transform.position;

			// Increment the Y value of the panel 
			Vector3 _incrementYPosition = 
				new Vector3(_currentUIPosition.x ,
					_currentUIPosition.y + scrollSpeed*Time.deltaTime* Mathf.Cos(Mathf.Deg2Rad * rotation),
					_currentUIPosition.z);

			// Change the transform position to the new one
			scrollTextGO.transform.position = _incrementYPosition;

			if (_incrementYPosition.y >= maxYlimit) //Stop scrolling if limit is reached
				isScrolling = false;
		}

		if (!isScrolling)
			continueText.enabled = true;
		
		if (Input.GetKeyDown (KeyCode.Space) && !isScrolling) {
			continueText.text="Loading";
			SceneManager.LoadScene (2);
			musicSystem.MainMusic ();
		}
	}

	void Setup(){
		isScrolling = true;
		rotation = gameObject.GetComponentInParent<Transform>().eulerAngles.x;
	}
}
