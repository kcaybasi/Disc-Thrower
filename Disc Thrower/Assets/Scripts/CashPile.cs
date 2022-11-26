using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CashPile : MonoBehaviour
{
    const string k_PlayerTag = "Player";
    [SerializeField] private ParticleSystem cashParticle;
    [SerializeField] private GameObject cashHolder;
    private bool _isCollected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(k_PlayerTag))
        {
            cashParticle.Play();
            cashHolder.transform.DOScale(Vector3.zero, .35f);
        }
    }

}
