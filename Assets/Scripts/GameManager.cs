using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null; //Estático para ser llamado por otro script
	public int level = 1; //Nivel en el que empieza el juego
	public float playerLife = 100;

	private BoardManager boardScript;
	void Awake()
	{
		if (instance == null)	//Revisa que no se haya instanciado el objeto
			instance = this;	//Asigna la instancia
		
		else if (instance != this)	//Destruye el objeto si está instanciado 
			Destroy (gameObject);	//con otro objeto

		//No se destruye el cargar la escena
		DontDestroyOnLoad (gameObject);

		//Agrega una referencia al boardManager
		boardScript = GetComponent<BoardManager> ();

		InitGame (); //Función de inicio del juego
	}

	//Inicia el juego en un determinado nivel
	void InitGame()
	{
		Debug.Log (level);
		boardScript.SetupScene (level);

	}


	public void GameOver()
	{
		SceneManager.LoadScene (0);
	}

	//Metodo de unity que se llama cada vez que una escena es cargada
	void OnLevelWasLoaded()
	{
		level++; 	
		InitGame ();
	}

	public float darNivel(){

		return level;
	}
}
