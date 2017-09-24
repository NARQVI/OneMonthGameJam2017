using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chaser : MonoBehaviour,DmgObjetc {
   
    public EnemyValues enemy;   
    public int num;
    public int life; // atributo de vida
    public LayerMask lay; 
    public GameObject player; // el jugador
	public DoorController door;
    public bool rand = false;
    public bool patrol;
    [SerializeField] private GameObject currentTarget;
    public bool chase = false;
    public bool attack = false;
    public GameObject[] waypoints;
    public float recoverytiem; // modela los frames de invulneravilidad
    NavMeshAgent agent;
    [SerializeField] bool recob;
    [SerializeField] private Collider[] Cchase;
    [SerializeField] private Collider[] cview;
    [SerializeField] private float view;
    [SerializeField] private float chases;
    private Transform trans;
    private Animator anim;
    private int waypointtarget;
    public float dis;

	public GameObject lifeFeedBack;
	Transform lifeFeedBackSpawnPoint;

	/* Atributos de Audio */
	[FMODUnity.EventRef]
	public string chaserAttackEvent; // Event Player Attack
	public string chaserMoveEvent; // Event Player Move 
	public string chaserDamageEvent; // Event Chaser Damage 

    // Use this for initialization
    void Start()
    {
        //inicializacion de variable
        life = enemy.life;
        waypointtarget = 0;
        num = 0;
        chases = enemy.chasedistance;
        view = enemy.viewdistance;
        recoverytiem = enemy.recoverytime;
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.acceleration = enemy.acceleration;
        agent.speed = enemy.maxspeed;
        agent.stoppingDistance = enemy.attackrange;

        recob = false;

		var children = gameObject.GetComponentsInChildren<Transform> ();
		foreach (var child in children) 
		{
			if(child.name=="LifeSpawnPointEn")
			{
				lifeFeedBackSpawnPoint = child.transform; //Gets SpawnPoint location
				break;
			}
		}
		

		/* Initialization for Audio Events */
		chaserAttackEvent = "event:/Enemies/Chaser/Attack"; // Event Attack 
		chaserMoveEvent = "event:/Enemies/Chaser/Move"; // Event Move
		chaserDamageEvent = "event:/Enemies/Chaser/Damage"; // Event Damage

    }

    // Update is called once per frame
    void Update () {
      
       if(life>0)
        {
 
        
        Cchase = Physics.OverlapSphere(trans.position, chases, lay);
        cview = Physics.OverlapSphere(trans.position, view, lay);

        if(cview.Length!=0 && Cchase.Length==0)
            {
                Vector3 look = new Vector3(player.transform.position.x, trans.position.y, player.transform.position.z);
                agent.destination = trans.position;
                anim.SetBool("alert", true);
                anim.SetBool("run", false);
                trans.LookAt(look);
            }
 
            if (Cchase.Length!=0 && !attack)
           {
            agent.destination = player.GetComponent<Transform>().position;
            chase = true;
            anim.SetBool("run", true);

            dis = Vector3.Distance(trans.position, player.transform.position);
            if (dis<=agent.stoppingDistance)
            {
                anim.SetBool("run", false);
                
                StartCoroutine(attime());
            }

        }
        else
        {
                if (patrol && Cchase.Length == 0 && cview.Length == 0)
                {
                    float distance = Vector3.Distance(trans.position, waypoints[num].transform.position);
                    if (distance >= agent.stoppingDistance)
                    {
                        agent.destination = waypoints[num].transform.position;

                        anim.SetBool("caminar", true);
                    }
                    else
                    {
                        if (num + 1 == waypoints.Length)
                        {
                            num = 0;
                        }
                        else
                        {
                            num++;
                        }
                    }

                }
                else
                {
                    agent.destination = trans.position;
                    anim.SetBool("caminar", false);
                }
      
         }

        }


	
        else

        {
            anim.SetBool("caminar", false);
            anim.SetBool("attack1", false);
          //  anim.SetBool("alert", false);
      
            // anim.SetBool("alive", false);
            StartCoroutine(destroid());

        }
    }

    /**
	 * Sound Methods
	 **/

    //Hace que suene el sonido cuando es atacado
    public void DamageSound()
	{
		FMODUnity.RuntimeManager.PlayOneShot(chaserDamageEvent, transform.position);

	}

	//Hace que suene el sonido del movimiento
	public void MoveSound()
	{

		FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(chaserMoveEvent); // Create a instance of the sound event 
		e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position)); // Give the position to correct listing in the stereo image

		e.start();
		e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 

	}

	//Hace que suene el sonido del ataque
	public void AttackSound()
	{

		FMODUnity.RuntimeManager.PlayOneShot (chaserAttackEvent, transform.position);

	}


	/**
	 * Other Methods
	 **/


    private IEnumerator attime()
    {
            anim.SetBool("attack1", true);
            attack = true;
            yield return new WaitForSeconds(2.3f);
            anim.SetBool("attack1", false);
     
        attack = false;
        
   
      
    }
    private IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1f);
    }


    private IEnumerator destroid()
    {
        yield return new WaitForSecondsRealtime(5);
		door.CountEnemy ();
        Destroy(gameObject);
    }

    private IEnumerator rectime()
    {
        recob = true;
        yield return new WaitForSecondsRealtime(recoverytiem);
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
    public void TakeDmg(int dmg)
    {
        if (!recob)
        {
            StartCoroutine(hii());
            life -= dmg;
            if (life + dmg >= 0)
                InstantiateLifeFeedBack(-dmg);
            StartCoroutine(rectime());
        }
    }
    private IEnumerator hii()
    {
        anim.SetTrigger("hit");
        yield return new WaitForSecondsRealtime(1);
        
    }


}
