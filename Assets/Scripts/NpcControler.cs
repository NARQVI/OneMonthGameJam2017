using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcControler : MonoBehaviour {


    [SerializeField] private bool lookat;  // variale que define si el jugador esta en vista del npc
    [SerializeField] private Transform trans;  // atributo que representa la posicion del objeto
    public GameObject player;  // el jugador
    public float speed; // velocidad con la que el npc se aleja del pj
	// Use this for initialization
	void Start () {           // inicializacion de las variables
        lookat = false;
        trans = GetComponent<Transform>();
      
    }
	
	// Update is called once per frame
	void Update () {
      

        if (lookat)   
        {
            Vector3 look = new Vector3(player.transform.position.x, trans.position.y, player.transform.position.z); //metodo que hace que el npc mire ahcia el jugador y se aleje de el en direccion contraria mientra
            trans.LookAt(look);                                                                                     // este se encuentre en el campo de vision
            trans.position -= trans.forward*Time.deltaTime*speed;
         //   Vector3 dir = trans.position - player.GetComponent<Transform>().position;
           // trans.Translate(dir * speed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))   // si el pj entra en el campo de vision se incializa el metodo para alejarse
        {
            lookat = true;
        }
                
    }
    private void OnTriggerExit(Collider other)
    { 
        lookat = false;   // si el pj sale de la vision el npc se detiene
    }
}
