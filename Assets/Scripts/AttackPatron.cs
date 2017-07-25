using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatron : MonoBehaviour {

    public int playerDamage;           // Damage points when hit to player
    public float timeInside;           // Time that player must be in contact to get hit again
    public float Secons;
    private float nextHit;             //Store global time to get hit
    private Animator animator;         //Thi object animator
    public bool onspikes;
    private Collider oth;
    private void Start()
   
    {
        nextHit = timeInside;
        animator = GetComponent<Animator>();
       StartCoroutine( up());
    }

    //Called when player enters to collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Checks if collides with the player      
        {
            oth = other;
            onspikes = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Checks if collides with the player      
        {

            onspikes = false ;
        }
    }
    //Called when player stays in the collider
 
    private IEnumerator up()
    {
        
        yield return new WaitForSeconds(Secons);

        if (animator != null)
        {
            animator.SetTrigger("PlayerInRange"); 
        }
        StartCoroutine(up());
    }
}
