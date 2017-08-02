using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chaser : MonoBehaviour {
   
    public int num = 0;
    public LayerMask lay;
    public GameObject player;
    public bool rand = false;
    GameObject currentTarget;
    public bool chase = false;
    public bool attack = false;
    [SerializeField] private Collider[] Cchase;
    [SerializeField] private Collider[] Cattack;

    private Transform trans;
    private Animator anim;
    public float dis;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        player = GameObject.Find("Player");
       
    }

    // Update is called once per frame
    void Update () {
       
        Cchase = Physics.OverlapSphere(trans.position, 5f, lay);
        Cattack = Physics.OverlapSphere(trans.position, 0.3f, lay);
        if (Cchase.Length!=0 && !attack)
        {
            Vector3 look = new Vector3(player.transform.position.x, trans.position.y, player.transform.position.z);
            trans.LookAt(look);
            trans.position += trans.forward * Time.deltaTime * 2;
            chase = true;
            anim.SetBool("run", true);

            dis = Vector3.Distance(trans.position, player.transform.position);
            if (dis<=2)
            {
                anim.SetBool("run", false);
                
                StartCoroutine(attime());
            }

        }
   //     else
    //    {
     //       chase = false;
      //      anim.SetBool("run", false);
       // }
        //if (chase)
       // {
        //    Vector3 look = new Vector3(player.transform.position.x, trans.position.y, player.transform.position.z);
         //   trans.LookAt(look);
          //  trans.position += trans.forward * Time.deltaTime * 2;
       // }
  
  
	}

    private IEnumerator attime()
    {
            anim.SetBool("attack", true);
            attack = true;
            yield return new WaitForSeconds(5f);
            anim.SetBool("attack", false);
     
        attack = false;
        
   
      
    }
    private IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1f);
    }

}
