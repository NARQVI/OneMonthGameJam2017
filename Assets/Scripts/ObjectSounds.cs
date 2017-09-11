using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesSound{


	private string objectSound; // Refence to  FMOD event


	public void AttackSound(string path)
	{
	
		objectSound = path;

		FMODUnity.RuntimeManager.PlayOneShot (objectSound);

	}

	public void UseSound(string path)
	{
		objectSound = path;

		FMODUnity.RuntimeManager.PlayOneShot (objectSound);
	}


}
