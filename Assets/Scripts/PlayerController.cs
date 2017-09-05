using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 *Class that models the behaviour of the player 
 */

public class PlayerController : MonoBehaviour,DmgObjetc {

    [SerializeField]
    public float moveSpeed = 15f;       //Object movement speed 
	public float restartDelayTime = 1f; //Tiempo de retardo para reiniciar la escena

	public float timeToHeal = 1f;
	public float runSpeed = 2f;
    public GameObject wepon;
    public float recoverytime; //tiempo de invulneravilidad luego de ser golpeado
    
	bool recob;
    Vector3 forward, right;
    Rigidbody rb;                       //This object rigid body
    Animator animator;                  //this object animator
    Vector3 movement;
	int life;           //Models the players life
	float nextHeal;		//Timer for counting the time btwn heals
	bool onChangeScene;	//Boolean for changing scenes

	/*GUI*/
	public Text lifeText;
	public Slider lifeSlider;
	public GameObject lifeFeedBack;
	Transform lifeFeedBackSpawnPoint;

	/* Atributos de Audio */
	[FMODUnity.EventRef]
	public string PlayerAttackEvent; // Event Player Attack
	public string PlayerMoveEvent; // Event Player Move 
	private float moveActualSpeed;
    // Use this for initialization
    void Start()
    {

        onChangeScene = false;

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        moveActualSpeed = moveSpeed;

        rb = GetComponent<Rigidbody>();     //Gets rigid body component
        animator = GetComponent<Animator>(); //Gets animator component

        life = GameManager.instance.playerLife;
        lifeSlider.value = life;
        lifeText.text = "Vida: " + life;

        recob = false; 

		lifeFeedBackSpawnPoint = GameObject.Find ("LifeSpawnPoint").transform; //Gets SpawnPoint location


        /* Initialization for Audio Events */
        PlayerAttackEvent = "event:/Player/Attack"; // Event Attack 
		PlayerMoveEvent = "event:/Player/Move"; // Event Move

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    //Check if space key is down
        {
            Attack();   //Calls attack method
            Debug.LogError("quizasww");
            wepon.GetComponent<AsteDmg>().attack();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  //Get horizontal input
        float vertical = Input.GetAxisRaw("Vertical");      //Get vertical input
		bool running = Input.GetKey(KeyCode.LeftShift);
        Move(horizontal,vertical,running);  //Calls Move method
    }

    //Moves the player in a given direction
	void Move(float h, float v,bool running)
    {
		if (running) {
			moveActualSpeed = runSpeed;
		}
		else
			moveActualSpeed = moveSpeed;
		
		Vector3 rightMovement = Vector3.back * moveActualSpeed * Time.deltaTime *h; //Calculates vectors to get the right feel in an isometric
		Vector3 upMovement = Vector3.right * moveActualSpeed * Time.deltaTime *v;
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        if(heading != Vector3.zero) //Only change when moving
            transform.forward = heading;

        movement = rightMovement + upMovement;  
        movement = movement.normalized * moveActualSpeed*Time.deltaTime; //Normalized for given the player the same velocity when two keys are press
		

		rb.MovePosition(transform.position+movement);   //Moves the object
        animator.SetFloat("Velocity",Mathf.Abs(Input.GetAxisRaw("Horizontal"))+Mathf.Abs( Input.GetAxisRaw("Vertical"))); //Set the value of "Velocity" in the animator
		animator.SetBool ("Run",running);


		if (h != 0 || v != 0) {

			FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(PlayerMoveEvent); // Create a instance of the sound event 
			e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position)); // Give the position to correct listing in the stereo image

			if (moveActualSpeed == runSpeed)
				e.setParameterValue ("Running", 1f); // Change the parameter when the player move on ice surface
			else if (moveActualSpeed == moveSpeed)
				e.setParameterValue ("Running", 0f); // Change the parameter when the player move on a diferent surface

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
    public void TakeDmg(int hit)
    {
        if(!recob)
        { 
        	life -= hit;
			InstantiateLifeFeedBack (-hit);
            StartCoroutine(recovery());
        }
        lifeText.text = "Vida: " + life;
		lifeSlider.value = life;
        if (life <= 0f)
        {
			GameManager.instance.GameOver (); // Calls GameOver function after 2seconds
        }
    }


    //Carga la última escena
    void Restart()
    {
        SceneManager.LoadScene(0);
    }

	//Se llama cuando se reinicia la escena
	private void OnDisable()
	{
		GameManager.instance.playerLife = life;
	}

	//Se llama cuando el jugador se queda dentro de una colision
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Exit") && !IsInvoking("Restart")) 
		{
			Invoke ("Restart", restartDelayTime);

		}

		if (other.CompareTag ("Heal"))  //Activa cuando el jugador entra en la estatua de curacion
			Heal();

		nextHeal = Time.time + timeToHeal;

	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Heal") && Time.time > nextHeal) { //Activa cuando el jugador entra en la estatua de curacion
			Heal();
			nextHeal = Time.time + timeToHeal;
		}
	}

	//Cura al jugador a una tasa de 10% de la vida actual
	void Heal()
	{
		int increment = Mathf.RoundToInt(life + life * 0.1f);
		int difference = Mathf.RoundToInt(life * 0.1f);
		if (increment > 100)
			life = 100;
		else
			life = increment;
		lifeText.text = "Vida: " + life;
		lifeSlider.value = life;
		InstantiateLifeFeedBack (difference);
	}

    //mettodo que da un tiempo de recuperacion anters de recivir mas daño
    private IEnumerator recovery()
    {
        recob = true;
        animator.SetTrigger("Hit");
        yield return new WaitForSecondsRealtime(recoverytime);
        recob = false;
    }

	private void InstantiateLifeFeedBack(int lifeLost)
	{
		GameObject lifeFB = null;

		if (lifeFeedBackSpawnPoint != null) 
		{
			lifeFB = (GameObject)Instantiate (lifeFeedBack, lifeFeedBackSpawnPoint.position, lifeFeedBackSpawnPoint.rotation);
			lifeFB.GetComponent<LifeFeedBack> ().lifeLost = lifeLost;
		}
		
	}

}
