using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victimAI : MonoBehaviour {


    public GameObject player;
    public float speed;
    public int numberofpoints;
    public LayerMask pjlayer;
    private Transform trans;
    [SerializeField] bool lookat;
    [SerializeField] bool move;
    public Transform[] points;
    [SerializeField] private int num;
    [SerializeField] private Transform tt;
    private bool target;
    // Use this for initialization
    void Start () {
        
        trans = GetComponent<Transform>();
        lookat = false;
        target = false;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 look = new Vector3(player.transform.position.x, trans.position.y, player.transform.position.z);
        Vector3 fwr = trans.TransformDirection(Vector3.forward);
        Collider[] col = Physics.OverlapSphere(trans.position, 1.2f,pjlayer);
        if(Physics.Raycast(trans.position,fwr,5, pjlayer))
        {
            lookat = true;
        }
        else
        {
            lookat = false;
        }
        if(lookat)
        {
            trans.LookAt(look);
        }
            
       if(col.Length!=0)
        {
           move = true;
            if(!target)
           {
               num = Random.Range(0, points.Length);
                 tt = points[num] as Transform;
                target = true;
            }
            
        }
        if(move)
        {
            
            float distance = Vector3.Distance(trans.position, tt.position);
            if(distance>2)
            {
                Move();
            }
            else
            {
                move = false;
                target = false;
            }
        }
    }
    public void Move()
    {
       
        trans.LookAt(tt.position);
        trans.position += trans.forward * speed * Time.deltaTime;
    }
}
