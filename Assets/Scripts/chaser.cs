using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chaser : MonoBehaviour {
   
    public int num = 0;
    public int life; // atributo de vida
    public LayerMask lay; 
    public GameObject player; // el jugador
    public bool rand = false;
    GameObject currentTarget;
    public bool chase = false;
    public bool attack = false;
    public float recoverytiem; // modela los frames de invulneravilidad
    NavMeshAgent agent;
    [SerializeField] bool recob;
    [SerializeField] private Collider[] Cchase;
    [SerializeField] private Collider[] Cattack;

    private Transform trans;
    private Animator anim;
    public float dis;

	/* Atributos de Audio */
	[FMODUnity.EventRef]
	public string ChaserAttackEvent; // Event Player Attack
	public string ChaserMoveEvent; // Event Player Move 

    // Use this for initialization
    void Start()
    {
        //inicializacion de variable
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        recob = false;
		/* Initialization for Audio Events */
		ChaserAttackEvent = "event:/Enemies/Chaser/Attack"; // Event Attack 
		ChaserMoveEvent = "event:/Enemies/Chaser/Move"; // Event Move

    }

    // Update is called once per frame
    void Update () {
       if(life>=0)
        {

        Cchase = Physics.OverlapSphere(trans.position, 5f, lay);
        Cattack = Physics.OverlapSphere(trans.position, 0.3f, lay);
        if (Cchase.Length!=0 && !attack)
        {
            //Vector3 look = new Vector3(player.transform.position.x, trans.position.y, player.transform.position.z);
            //trans.LookAt(look);
            // trans.position += trans.forward * Time.deltaTime * 2;
            agent.destination = player.GetComponent<Transform>().position;
            chase = true;
            anim.SetBool("run", true);

			FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(ChaserMoveEvent); // Create a instance of the sound event 
			e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position)); // Give the position to correct listing in the stereo image

            dis = Vector3.Distance(trans.position, player.transform.position);
            if (dis<=agent.stoppingDistance)
            {
                anim.SetBool("run", false);
                
                StartCoroutine(attime());
            }

        }
        else
        {
            agent.destination = trans.position;
        }

        }
       else

        {
            anim.SetBool("run", false);
            anim.SetBool("attack", false);
            anim.SetBool("alive", false);
            StartCoroutine(destroid());
            
        }
	}

    private IEnumerator attime()
    {
            anim.SetBool("attack", true);
            attack = true;
            yield return new WaitForSeconds(2.3f);
            anim.SetBool("attack", false);
     
        attack = false;
        
   
      
    }
    private IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1f);
    }

    public void takeDmg(int dmg)
    {
        if(!recob)
        {
            life -= dmg;
            StartCoroutine(rectime());
        }
 
        
    }
    private IEnumerator destroid()
    {
        yield return new WaitForSecondsRealtime(5);
        Destroy(gameObject);
    }

    private IEnumerator rectime()
    {
        recob = true;
        yield return new WaitForSecondsRealtime(recoverytiem);
        recob = false;
    }
}
