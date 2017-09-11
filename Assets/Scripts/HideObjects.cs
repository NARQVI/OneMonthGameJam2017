using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HideObjects : MonoBehaviour
{

    public Transform WatchTarget;
    public LayerMask OccluderMask;

	private RaycastHit[] hits;

    void Start()
    {
		hits = null;
    }

    void Update()
    {

		Debug.DrawRay (WatchTarget.position,transform.position-WatchTarget.position,Color.magenta);

		if (hits != null) {
			foreach (RaycastHit hit in hits) 
			{
				Material[] actualMat = hit.collider.gameObject.GetComponent<Renderer>().materials;

				foreach (Material m in actualMat) {
					Color matColor = m.color;
					matColor.a = 1f;
					m.color = matColor;
				}
			}
		}

		hits = Physics.RaycastAll (WatchTarget.position,transform.position-WatchTarget.position,
			Vector3.Distance(WatchTarget.position,transform.position)*2.5f,OccluderMask);



		foreach (RaycastHit hit in hits) 
		{
			Material[] actualMat = hit.collider.gameObject.GetComponent<Renderer>().materials;

			foreach (Material m in actualMat) {
				Color matColor = m.color;
				matColor.a = 0.2f;
				m.color = matColor;
			}
				
		}
    }


}
