using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confeti : MonoBehaviour
{

    [SerializeField] private ParticleSystem confetiParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            confetiParticle.Play();
        }
    }
}

