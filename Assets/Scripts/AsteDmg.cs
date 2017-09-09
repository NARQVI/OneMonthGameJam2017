using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteDmg : MonoBehaviour {

    public int dmg;
    public GameObject oi;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void attack()
    {
        StartCoroutine(ative());
        Debug.LogError("quizas");
    }

    private IEnumerator ative()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(1.5f);
        GetComponent<SphereCollider>().enabled = false;

    }
    private void OnTriggerEnter(Collider other)
    {

		other.gameObject.GetComponent<DmgObjetc>().TakeDmg(dmg);
        /**try
        {
            //  other.gameObject.GetComponent<tdmg>().takeDmg(dmg);
            other.gameObject.GetComponent<DmgObjetc>().TakeDmg(dmg);
        }     
        catch
        {
            Debug.Log("no hay daño");
        }*/
    }

}
