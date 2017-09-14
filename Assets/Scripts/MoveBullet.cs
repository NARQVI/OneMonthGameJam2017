using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Class that models the moving of a bullet 
 */

public class MoveBullet : MonoBehaviour {

	public Vector3 speed;                 //Moving speed

    private Rigidbody rigidBody;        //This object rigid body
   

    //Get call when the object is instantiated
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
		rigidBody.velocity = speed;
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
