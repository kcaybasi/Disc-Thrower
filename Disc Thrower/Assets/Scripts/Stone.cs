using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Stone : MonoBehaviour
{ 
    private ParticleSystem _gibletParticle;
    private Color _stoneColor;
    [SerializeField] private int hitCount;
    public int HitCount => hitCount;
    [SerializeField] private TextMeshProUGUI hitCountText;
    private Tower _tower;

    private void Awake()
    {
        _tower = transform.parent.GetComponent<Tower>();
        _gibletParticle = transform.parent.GetChild(0).GetComponent<ParticleSystem>();
        _stoneColor = GetComponent<MeshRenderer>().material.color;
        hitCountText.text = hitCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            other.GetComponent<Disc>().SendDiscBack();
            
            var gibletParticleMain = _gibletParticle.main;
            gibletParticleMain.startColor = _stoneColor;
            
            other.GetComponent<Collider>().enabled = false;
            UpdateHitCount();
            _tower.CheckIfTowerDestroyed();
            if (hitCount==0)
            {
                _gibletParticle.Play();
                transform.DOScale(Vector3.zero, .35f);
            }
        }
    }

    private void UpdateHitCount()
    {
        hitCount--;
        hitCountText.text = hitCount.ToString();
    }
}
