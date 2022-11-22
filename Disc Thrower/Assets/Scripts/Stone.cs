using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stone : MonoBehaviour
{ 
    private ParticleSystem _gibletParticle;

    private void Awake()
    {
        _gibletParticle = transform.parent.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            
            _gibletParticle.Play();
            transform.DOScale(Vector3.zero, .25f);
            other.GetComponent<Collider>().enabled = false;
        }
    }
}
