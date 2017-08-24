using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getpoints : MonoBehaviour {


    public Transform waypoints;
    public ArrayList arr;
    public Transform[] po;
    // Use this for initialization
    void Start () {
        arr = new ArrayList();
	}
	
	public Transform[] getpoint()
    {
        
        foreach(Transform child in transform)
        {
            arr.Add(child);
        }
        
        for(int i=0;i<arr.Count;i++)
        {
            po[i] = (Transform)arr[i];
        }
        return po;
    }
}
