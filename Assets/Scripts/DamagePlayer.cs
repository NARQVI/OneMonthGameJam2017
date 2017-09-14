using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Class that models the behaviour of a collision that
 * deals damage to the player
 */

public class DamagePlayer : MonoBehaviour {

    public int playerDamage;           // Damage points when hit to player
    public float timeInside;           // Time that player must be in contact to get hit again

    private float nextHit;             //Store global time to get hit
    private Animator animator;         //Thi object animator

    private void Start()
    {
        nextHit = timeInside;
        animator = GetComponent<Animator>();
    }

    //Called when player enters to collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Checks if collides with the player      
		{					
            other.GetComponent<PlayerController>().TakeDmg(playerDamage); // Callsd hit function in Player Controlelr script
			other.GetComponent<PlayerController>().DamageSound (); // Suena el daño al jugador
            if (animator != null)
                animator.SetTrigger("PlayerInRange");
        }
		nextHit = Time.time+timeInside;
    }

    //Called when player stays in the collider
    private void OnTriggerStay(Collider other)
    {
		
		if (other is SphereCollider && other.CompareTag("Player") && Time.time > nextHit) // Checks if collides with the player          
		{
            nextHit = Time.time + timeInside;
            other.GetComponent<PlayerController>().TakeDmg(playerDamage); // Calls hit function in Player Controlelr script
			other.GetComponent<PlayerController>().DamageSound (); // Suena el daño al jugador
            if (animator != null)
                animator.SetTrigger("PlayerInRange");
        }
    }



}
