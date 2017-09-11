using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour {


	public void startTextScene()
	{
		StartCoroutine(AguanteElBollo()); 
	}

	//Hace que suene el sonido del boton, recibe por parametro el path del evento en FMOD
	public void PlaySound(string path)
	{

		FMODUnity.RuntimeManager.PlayOneShot (path);

	}

	//Segundos para que suene el sonido del boton :V
	IEnumerator AguanteElBollo()
	{
		
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene (1);
	}
}
