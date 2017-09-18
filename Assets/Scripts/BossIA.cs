using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossIA : MonoBehaviour,DmgObjetc {

    public float speed; //velocidad del boos
    public int attackDmg; // daño del boss
    public int bossLife; // vida del boss
    public float attackRange; // rango de ataque
    public bool alive; // estado si esta vivo
    public LayerMask lay;
    [SerializeField] private bool active; // determina si el boss esta activo
    [SerializeField] private GameObject player;
    [SerializeField] private float distance; // distancia del boss a un objeto dado
    private Animator anim;
    private NavMeshAgent agent;
	/* Atributos de Audio */
	[FMODUnity.EventRef]
	public string BossAttackEvent; // Event Player Attack
	public string BossMoveEvent; // Event Player Move 
	public string BossDamageEvent; // Event Player Move 
    [SerializeField] private Collider[] attack;

    public int atp;
    private Transform trans;

	// Use this for initialization
	void Start () {
        active = false;
        player = GameObject.Find("Player");
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        alive = true;
        anim.SetInteger("life", bossLife);
        agent = GetComponent<NavMeshAgent>();
        atp = 0;
        /* Initialization for Audio Events */
        BossAttackEvent = "event:/Enemies/Boss/Attack"; // Event Attack 
		BossMoveEvent = "event:/Enemies/Boss/Move"; // Event Move
		BossDamageEvent = "event:/Enemies/Boss/Damage"; // Event Move
    }
	
	// Update is called once per frame
	void Update () {
        
        if(bossLife > 0)
        {

        if (active && alive)
        {
            attack = Physics.OverlapSphere(trans.position, 5f, lay);
            agent.destination = player.GetComponent<Transform>().position;
            if ( attack.Length!=0)
            {
                    anim.SetBool("walk", false);
 
                    if (atp==0)
                    {
                        StartCoroutine(attackrig());
                    }
                    else if(atp==1)
                    {
                      
                    }
                    else if(atp==2)
                    {
                        StartCoroutine(attackleft());
                    }
                    else
                    {
                        atp = 0;
                    }
              
                    agent.destination = trans.position;
                    // Vector3 look = new Vector3(player.transform.position.x, trans.position.y, player.transform.position.z);
                    // trans.LookAt(look);
                    // trans.position = Vector3.MoveTowards(trans.position, player.GetComponent<Transform>().position, speed);
                  
                    AttackSound();
                }
            else
            {
                    agent.destination = player.GetComponent<Transform>().position;
                    anim.SetBool("walk", true);
                anim.SetBool("attack1",false);
                    anim.SetBool("attack2", false);
                    atp = 0;
                    MoveSound();
                }
 
   
          
          
        }

        }
        else
        {
            StartCoroutine(destroid());
        }

    }

    public void setActive(bool ss)
    {
        active = ss;
    }
    private IEnumerator destroid()
    {
        yield return new WaitForSecondsRealtime(5);
        Destroy(gameObject);
    }
    private IEnumerator attackdelay(float n)
    {
        anim.SetFloat("attack", n);
        yield return new WaitForSecondsRealtime(1f);
        anim.SetFloat("attack", 0);
    }

    public void TakeDmg(int dmg)
    {
        bossLife -= dmg;
        DamageSound();
    }

	/**
	 * Sound Methods
	 **/

	//Hace que suene el sonido cuando es atacado
	public void DamageSound()
	{
		FMODUnity.RuntimeManager.PlayOneShot(BossDamageEvent, transform.position);

	}

	//Hace que suene el sonido del movimiento
	public void MoveSound()
	{

		FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(BossMoveEvent); // Create a instance of the sound event 
		e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position)); // Give the position to correct listing in the stereo image
	
		e.start();
		e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 

	}

	//Hace que suene el sonido del ataque
	public void AttackSound()
	{
		FMODUnity.RuntimeManager.PlayOneShot(BossAttackEvent, transform.position);
	}

    private IEnumerator attackrig()
    {
        anim.SetBool("attack1", true);
        yield return new WaitForSecondsRealtime(3);
        anim.SetBool("attack1", false);
        atp++;
    }

    private IEnumerator attackleft()
    {
        anim.SetBool("attack2", true);
        yield return new WaitForSecondsRealtime(3);
        anim.SetBool("attack2", false);
        atp++;
    }
}
