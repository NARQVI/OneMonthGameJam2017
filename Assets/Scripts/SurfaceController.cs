using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class that models the behaviour of any surface
 * */

public class SurfaceController : MonoBehaviour {

    public float velocity = 10f;                //Velocity that the player will have when enters the surface

    private float origVelocity;                 //Original velocity of the player
    private PlayerController playerController;  //Reference to PlayerController script, attached to player object

    //Call at start of game
    void Start()
    {
        playerController=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); //Gets reference
        origVelocity = playerController.moveSpeed;  //Stores original velocity of player
    }

    //Call when anything enters the surface
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))   //Checks if is the player 
        {
            playerController.moveSpeed = velocity;      //sets moveSpeed variable in PlayerController script
        }
    }

    //Call when anythin exit the surface
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  //Checks if is the player
        {
            playerController.moveSpeed = origVelocity;  //sets moveSpeed variable in PlayerController script
        }
    }
}
