using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chaser : MonoBehaviour {
   
    public int num = 0;
    public GameObject player;
    public bool rand = false;
    GameObject currentTarget;
    NavMeshAgent agent;
    public bool chase = false;

    // Use this for initialization
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update () {
		
        if(chase)
        {
            agent.destination = player.transform.position;
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            chase = true;
        }
    }
}
