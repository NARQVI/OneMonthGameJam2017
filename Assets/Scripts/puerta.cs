using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour {

    public GameObject[] enemigos;
    private Animator anim;
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        int cont = 0;
     for(int i=0; i<enemigos.Length;i++)
        {
            if(enemigos[i]==null)
            {
                cont++;
            }
        }
     if(cont>=enemigos.Length)
        {
            StartCoroutine(open());
        }
	}

    private IEnumerator open()
    {
        anim.SetTrigger("open");
        anim.SetTrigger("open1");
        yield return new WaitForSecondsRealtime(1);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<puerta>().enabled = false;
    }
}
