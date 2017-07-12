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

	/* Atributos de Audio */
	[FMODUnity.EventRef]
	public string PlayerAttackEvent; // Event Player Attack
	public string PlayerMoveEvent; // Event Player Move 

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


		/* Initialization for Audio Events */
		PlayerAttackEvent = "event:/Player/Attack"; // Event Attack 
		PlayerMoveEvent = "event:/Player/Move"; // Event Move

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
	

		if (h != 0 || v != 0) {

			FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(PlayerMoveEvent); // Create a instance of the sound event 
			e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position)); // Give the position to correct listing in the stereo image

			if (moveSpeed == 5)
				e.setParameterValue ("Ice", 1f); // Change the parameter when the player move on ice surface
			else
				e.setParameterValue ("Ice", 0f); // Change the parameter when the player move on a diferent surface

			e.start();
			e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 

		}

    }

    //Makes the player attack
    void Attack()
    {
        animator.SetTrigger("Attack"); //Sets the animation
		FMODUnity.RuntimeManager.PlayOneShot(PlayerAttackEvent, transform.position);
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
