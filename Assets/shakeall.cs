using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeall : MonoBehaviour {

    public GameObject player;
    public GameObject cam;
    public shakeit sc;
    private void Start()
    {
       sc= cam.GetComponent<shakeit>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Player"))
        sc.enabled = true; 
        
    }
}
