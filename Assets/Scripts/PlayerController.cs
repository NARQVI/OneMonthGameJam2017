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
    
	bool onChangeScene;	//Boolean for changing scenes
	bool recob;
    Vector3 forward, right;
	Vector3 movement;
    Rigidbody rb;                       //This object rigid body
    Animator animator;                  //this object animator

	int life;           //Models the players life
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float nextHeal;		//Timer for counting the time btwn heals
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.

	/*GUI*/
	public Text lifeText;
	public Slider lifeSlider;
	public GameObject lifeFeedBack;
	Transform lifeFeedBackSpawnPoint;

	/* Atributos de Audio */
	[FMODUnity.EventRef]
	public string playerAttackEvent; // Event Player Attack
	public string playerMoveEvent; // Event Player Move 
	public string playerDamageEvent; // Event player Damage
	private float moveActualSpeed;
	public string playerHealSound; // Refence to  FMOD event

	void Awake()
	{
		floorMask = LayerMask.GetMask ("terran");
	}


    // Use this for initialization
    void Start()
    {

        onChangeScene = false;
	
        moveActualSpeed = moveSpeed;

        rb = GetComponent<Rigidbody>();     //Gets rigid body component
        animator = GetComponent<Animator>(); //Gets animator component

        life = GameManager.instance.playerLife;
        lifeSlider.value = life;
        lifeText.text = "Vida: " + life;

        recob = false; 

		lifeFeedBackSpawnPoint = GameObject.Find ("LifeSpawnPoint").transform; //Gets SpawnPoint location


        /* Initialization for Audio Events */
        playerAttackEvent = "event:/Player/Attack"; // Event Attack 
		playerMoveEvent = "event:/Player/Move"; // Event Move
		playerDamageEvent = "event:/Player/Damage"; // Event Damage
		playerHealSound = "event:/Player/Heal";

 
    }

    private void Update()
    {
		
		if (Input.GetMouseButtonDown(0))    //Check if left mouse button is down
        {
            Attack();   //Calls attack method
           
            wepon.GetComponent<AsteDmg>().attack();
        }
	
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  //Get horizontal input
        float vertical = Input.GetAxisRaw("Vertical");      //Get vertical input
		bool running = Input.GetKey(KeyCode.LeftShift);

		if (Mathf.Abs (rb.velocity.y) >= 10)
			GameManager.instance.GameOver ();

		Move (horizontal, vertical, running);

        Turning ();
    }

	/**
	 * Sound Methods
	 **/

	//Hace que suene el sonido cuando es atacado
	public void DamageSound()
	{
		FMODUnity.RuntimeManager.PlayOneShot(playerDamageEvent, transform.position);

	}

	//Hace que suene el sonido del movimiento, 1 == Correr, 0 == caminar
	public void MoveSound(int velocidad)
	{
		
		FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(playerMoveEvent); // Create a instance of the sound event 
		e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position)); // Give the position to correct listing in the stereo image

		if (velocidad == 1)
			e.setParameterValue ("Running", 1f); // Change the parameter when the player move on ice surface
		else if (velocidad == 0)
			e.setParameterValue ("Running", 0f); // Change the parameter when the player move on a diferent surface

		e.start();
		e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 

	}

	//Hace que suene el sonido del ataque
	public void AttackSound()
	{
		FMODUnity.RuntimeManager.PlayOneShot(playerAttackEvent, transform.position);
	}

	//Hace que suene el efecto de curación
	public void HealSound()
	{

		FMODUnity.RuntimeManager.PlayOneShot (playerHealSound, transform.position);

	}

	/**
	 * Other Methods
	**/

    //Moves the player in a given direction
	void Move(float h, float v,bool running)
    {
		if (running) {
			moveActualSpeed = runSpeed;
		}
		else
			moveActualSpeed = moveSpeed;
		
		Vector3 upMovement = transform.forward * moveActualSpeed * Time.deltaTime *v;

		rb.MovePosition (transform.position + upMovement);
       
		if (h == 0) {
			animator.SetFloat ("Velocity", Mathf.Abs (Input.GetAxisRaw ("Horizontal")) + Mathf.Abs (Input.GetAxisRaw ("Vertical"))); //Set the value of "Velocity" in the animator
			animator.SetBool ("Run", running);
		}

		if (h != 0 || v != 0) {

			/**
			 * NO BORRAR PRUEBAS PARA SER MAS ORDENADOS EN CURSO
			FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(PlayerMoveEvent); // Create a instance of the sound event 
			e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position)); // Give the position to correct listing in the stereo image

			if (moveActualSpeed == runSpeed)
				e.setParameterValue ("Running", 1f); // Change the parameter when the player move on ice surface
			else if (moveActualSpeed == moveSpeed)
				e.setParameterValue ("Running", 0f); // Change the parameter when the player move on a diferent surface

			e.start();
			e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 
			*/

			//MoveSound ();

		}

    }

    //Makes the player attack
    void Attack()
    {
        animator.SetTrigger("Attack"); //Sets the animation
		//AttackSound();
    }

    //Makes the calculation for a hit to the player given for an enemy
    public void TakeDmg(int hit)
    {

        if(!recob)
        { 
        	life -= hit;
			InstantiateLifeFeedBack (-hit);
            StartCoroutine(recovery());
			//DamageSound (); // Play Damage Sound
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

		//Activa cuando el jugador entra en la estatua de curacion
		if (other.CompareTag ("Heal")&& life<100) 
		{
			Heal ();
		}

		nextHeal = Time.time + timeToHeal;

	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Heal") && Time.time > nextHeal && life<100) { //Activa cuando el jugador entra en la estatua de curacion
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
		HealSound ();
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

	//Makes player to face the coursor mouse
	void Turning()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			// Set the player's rotation to this new rotation.
			rb.MoveRotation (newRotation);
		}
	}

}
