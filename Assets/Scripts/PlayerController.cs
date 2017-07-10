using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 *Class that models the behaviour of the player 
 */

public class PlayerController : MonoBehaviour {

    [SerializeField]
    public float life = 100f;           //Models the players life
    public Text lifeText;               //Text with player life
    public float moveSpeed = 15f;       //Object movement speed 

    Vector3 forward, right;
    Rigidbody rb;                       //This object rigid body
    Animator animator;                  //this object animator
    Vector3 movement;
    // Use this for initialization
    void Awake()
    {
        
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        lifeText.text = "Life: " + life;

        rb = GetComponent<Rigidbody>();     //Gets rigid body component
        animator = GetComponent<Animator>(); //Gets animator component
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    //Check if space key is down
            Attack();   //Calls attack method
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  //Get horizontal input
        float vertical = Input.GetAxisRaw("Vertical");      //Get vertical input
        Move(horizontal,vertical);  //Calls Move method
    }

    //Moves the player in a given direction
    void Move(float h, float v)
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime *h; //Calculates vectors to get the right feel in an isometric
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime *v;
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;

        movement = rightMovement + upMovement;  
        movement = movement.normalized * moveSpeed*Time.deltaTime; //Normalized for given the player the same velocity when two keys are press
        rb.MovePosition(transform.position+movement);   //Moves the object
        animator.SetFloat("Velocity",Mathf.Abs(Input.GetAxisRaw("Horizontal"))+Mathf.Abs( Input.GetAxisRaw("Vertical"))); //Set the value of "Velocity" in the animator
       

    }

    //Makes the player attack
    void Attack()
    {
        animator.SetTrigger("Attack"); //Sets the animation
    }

    //Makes the calculation for a hit to the player given for an enemy
    public void Hit(float hit)
    {
        life -= hit;
        if (life <= 0f)
        {
            lifeText.text = "Dead";
            Invoke("GameOver", 2f); // Calls GameOver function after 2seconds
        }
        else
            lifeText.text = "Life: " + life;

    }

    //Restart the game
    void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
