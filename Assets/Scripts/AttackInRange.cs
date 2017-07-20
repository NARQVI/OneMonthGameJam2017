using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sets animation and attack when player is in range
 */
public class AttackInRange : MonoBehaviour {

    public  float attackDistance = 10f; //Max distance for object to attack
   
    public float time;                 // tiempo de que toma a la estatua atacar

    public float attackTime;          //timer que se activa caundo el jugador entra en la zona de ataque
    private bool atacar;              // boolean que indica si la estatua debe atacar
	private Transform player;            //Player's transform (Object to attack)
  
    private Transform collider;       //tranform del child que tiene el collider de ataque
    Animator animator;                  //This object animator
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        atacar = false;
		player = GameObject.FindGameObjectWithTag ("Player").transform; //Encuentra al objeto con la etiqueta dada
        collider = transform.Find("Circle_001");
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            
            atacar = true;  // true si el jugador entra en rango
        }
        else
        {
            collider.GetComponent<BoxCollider>().enabled = false; // mientras el jugador no este en el rango de ataque el collider de daño esta apagado
        }
        if(atacar)
        {
            attackTime += 1 * Time.deltaTime;                     // metodo que cuenta el tiempo entre la entrada del jugador a la zona y el ataque actual de la estatua
            if(attackTime >= time)                                // luego de que el timer se cumpla se corre la animacion y la estatua hace daño, resetiando el contador de neuvo a 0 y el bool a false
            {
                animator.SetTrigger("PlayerInRange");
                
                collider.GetComponent<BoxCollider>().enabled = true ;
                atacar = false;
                attackTime = 0;
              
            }
        }
    
	}
}
