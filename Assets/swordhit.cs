using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordhit : MonoBehaviour {


    public int dmg;
    public GameObject player;
    private int cont;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            cont++;
            if(cont>=100)
            {
                player.GetComponent<PlayerController>().Hit(dmg);
                cont = 0;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            cont = 0;
        }
    }

    public void acol()
    {
        GetComponent<Collider>();
    }
    public void pcol()
    {
        GetComponent<Collider>().enabled = false;
    }
}
