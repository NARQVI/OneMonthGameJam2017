using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcControler : MonoBehaviour {


    [SerializeField] private bool lookat;
    private Transform trans;

    private GameObject player;
	// Use this for initialization
	void Start () {
        lookat = false;
        trans = GetComponent<Transform>();
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
        if(lookat)
        {
            Vector3 look = new Vector3(player.transform.position.x, trans.position.y, player.transform.position.z);
            trans.LookAt(look);
            trans.position -= trans.forward*Time.deltaTime*2f;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            lookat = true;
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        lookat = false;
    }
}
