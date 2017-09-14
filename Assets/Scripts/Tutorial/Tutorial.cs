using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {


    public int Order;
    [TextArea(3,10)]
    public string Explanation;

	// Use this for initialization
	void Awake ()
    {

        TutorialManager.Instance.Tutorials.Add(this);
       

	}
	
    public virtual void CheckifHappening( ){ }
	
}
