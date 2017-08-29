using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour {

    public float speed;
    public int attackDmg;
    public int bossLife;
    public float attackRange;
    public bool alive;
    [SerializeField] private bool active;
    [SerializeField] private GameObject player;
    [SerializeField] private float distance;
    private Animator anim;

    private Transform trans;

	// Use this for initialization
	void Start () {
        active = false;
        player = GameObject.Find("Player");
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        alive = true;
        anim.SetInteger("life", bossLife);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (active && alive)
        {
            distance = Vector3.Distance(trans.position, player.GetComponent<Transform>().position);
            if(distance > attackRange)
            {

            Vector3 look = new Vector3(player.transform.position.x, trans.position.y, player.transform.position.z);
            trans.LookAt(look);
            trans.position = Vector3.MoveTowards(trans.position, player.GetComponent<Transform>().position, speed);
            anim.SetBool("walk", true);

            }
            else
            {
                anim.SetBool("walk", false);
            }
            if(bossLife<=0)
            {
                alive = false;
            }
        }
        else if(!alive)
        {
            anim.SetBool("walk", false);
            anim.SetBool("death", true);
            StartCoroutine(destroid());  
        }
	}


    public void takeDmg(int dmg)
    {
        bossLife -= dmg;
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
}
