using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatterOnCollision : MonoBehaviour {
    public GameObject replacement;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.Instantiate(replacement, transform.position, transform.rotation);

        Destroy(gameObject);
    }

}
