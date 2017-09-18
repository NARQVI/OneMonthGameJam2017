using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStar : MonoBehaviour {

    // Use this for initialization
    public GameObject  boss;
	void Start () {
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Player"))
        {
            boss.GetComponent<BossIA>().setActive(true);
        }
    }
}
