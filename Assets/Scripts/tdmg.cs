using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tdmg : MonoBehaviour {

    public GameObject io;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDmg(int dmg)
    {
        io.GetComponent<chaser>().takeDmg(dmg);
    }
		
}
