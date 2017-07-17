using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sets animation and attack when player is in range
 */
public class AttackInRange : MonoBehaviour {

    public  float attackDistance = 10f; //Max distance for object to attack
    public Transform player;            //Player's transform (Object to attack)

    Animator animator;                  //This object animator
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
            animator.SetTrigger("PlayerInRange");
	}
}
