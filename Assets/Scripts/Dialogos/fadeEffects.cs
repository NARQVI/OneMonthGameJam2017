using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeEffects : MonoBehaviour
{
    public Button btn;

    void Start()
    {
        btn = GameObject.Find("Button").GetComponent<Button>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            btn.gameObject.SetActive(true);
        }
    }
}
