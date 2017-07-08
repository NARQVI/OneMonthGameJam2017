﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour {

    public float speed;                 //Moving speed

    private Rigidbody rigidBody;        //This object rigid body
   

    //Get call when the object is instantiated
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = transform.forward * speed;
    }

    //Called when contact with other collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")) //Checks if collides with a wall
            Destroy(gameObject);
        else if (other.CompareTag("Player")) // Checks if collides with the player          
            Destroy(gameObject);     // Destroy this object
        
    }
}
