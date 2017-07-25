using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public GameObject gameManager; //prefab de GameManager

	void Awake()
	{
		if (GameManager.instance == null)
			Instantiate (gameManager); //Instancia el gameManager
	}
}
