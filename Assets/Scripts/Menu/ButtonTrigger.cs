using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ButtonTrigger : MonoBehaviour {

    public GameObject CanvasObject;

   

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("Player"))
           
            {
            CanvasObject.SetActive(true);

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        CanvasObject.SetActive(false);
    }
}
    




