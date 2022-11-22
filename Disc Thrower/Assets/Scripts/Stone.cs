using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stone : MonoBehaviour
{ 
    private ParticleSystem _gibletParticle;
    private Color _stoneColor;

    private void Awake()
    {
        _gibletParticle = transform.parent.GetChild(0).GetComponent<ParticleSystem>();
        _stoneColor = GetComponent<MeshRenderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            
            var gibletParticleMain = _gibletParticle.main;
            gibletParticleMain.startColor = _stoneColor;
            _gibletParticle.Play();
            transform.DOScale(Vector3.zero, .25f);
            other.GetComponent<Collider>().enabled = false;
        }
    }
}
