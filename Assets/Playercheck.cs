using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercheck : MonoBehaviour {

    public GameObject player;


	
	// Update is called once per frame
	void Update () {

        player.GetComponent<PlayerController>().enabled = true; // se asegura que el control del pj siempre este activo
        
	}
}
