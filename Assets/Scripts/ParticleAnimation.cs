using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAnimation : MonoBehaviour
{
    public ParticleSystem ParticlePrefab;


    private void OnCollisionEnter(Collision collision)
    {
        ParticlePrefab.Play();
    }

    private void OnCollisionExit(Collision collision)
    {
        ParticlePrefab.Stop();
    }




}
    