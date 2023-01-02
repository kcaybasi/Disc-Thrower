using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;
using TMPro;

public class BonusCube : MonoBehaviour
{
    public int HitCount { get; set; }
    [SerializeField] private ParticleSystem gibletParticle;
    public TextMeshProUGUI hitCountText;
    private Color _bonusCubeColor;
    private void Awake()
    {
        _bonusCubeColor = GetComponent<MeshRenderer>().material.color;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            other.GetComponent<Disc>().SendDiscBack();
            
            var gibletParticleMain = gibletParticle.main;
            gibletParticleMain.startColor = _bonusCubeColor;
            
            other.GetComponent<Collider>().enabled = false;
            UpdateHitCount();
            if (HitCount==0)
            {
                gibletParticle.Play();
                transform.DOScale(Vector3.zero, .35f);
            }
        }
    }
    
    private void UpdateHitCount()
    {
        HitCount--;
        hitCountText.text = HitCount.ToString();
    }
}
