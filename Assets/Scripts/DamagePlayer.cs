﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public int playerDamage;           // Damage points when hit to player
    public float timeInside;           // Time that player must be in contact to get hit again

    private float nextHit;             //Store global time to get hit

    private void Start()
    {
        nextHit = timeInside;
    }

    //Called when player enters to collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Checks if collides with the player          
            other.GetComponent<PlayerController>().Hit(playerDamage); // Callsd hit function in Player Controlelr script
    }

    //Called when player stays in the collider
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time > nextHit) // Checks if collides with the player          
        {
            nextHit = Time.time + timeInside;
            other.GetComponent<PlayerController>().Hit(playerDamage); // Calls hit function in Player Controlelr script
        }
    }



}
