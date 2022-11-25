using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HyperCasual.Runner;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<Stone> _stones;
    private int _hitCountForReward;
    [SerializeField] private ParticleSystem cashBillParticle;
    [SerializeField] private List<GameObject> cashObjects = new List<GameObject>();

    private void Start()
    {
        GetHitNeededHitCount();
    }

    void GetHitNeededHitCount()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Stone>())
            {
                Stone stone = child.GetComponent<Stone>();
                _hitCountForReward += stone.HitCount;
            }
        }
    }

    public void CheckIfTowerDestroyed()
    {
        _hitCountForReward--;
        if (_hitCountForReward==0)
        {
            cashBillParticle.Play();
            SendCashToPlayer();
        }
    }

    void SendCashToPlayer()
    {
        foreach (var cashObject in cashObjects)
        {
           
            cashObject.transform.DOScale(Vector3.zero, .85f);
        }
    }

}
