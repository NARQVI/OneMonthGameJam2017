using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSounds : MonoBehaviour {

	public void AttackSound(string path)
	{

		FMODUnity.RuntimeManager.PlayOneShot (path);
		Debug.Log ("Acá está el evento " + path);

	}

	public void UseSound(string path)
	{

		FMODUnity.RuntimeManager.PlayOneShot (path);
		Debug.Log ("Acá está el evento " + path);
	}


}
