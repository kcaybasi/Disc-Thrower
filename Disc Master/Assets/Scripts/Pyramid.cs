using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Pyramid : MonoBehaviour
{
    private ParticleSystem _smokeParticle;
    private Color _stoneColor;
    [SerializeField] private int hitCount;
    public int HitCount => hitCount;
    [SerializeField] private TextMeshProUGUI hitCountText;
    [SerializeField] private GameObject mysteryBox;
    [SerializeField] private GameObject feedbackTextGameObject;
    [SerializeField] private ParticleSystem dolarBillParticle;
    private TextMeshProUGUI _feedbackTextMeshProUGUI;
    string _feedbackText;
    List<string> _feedbackTexts = new List<string>();


    private void Awake()
    {
        _smokeParticle = transform.parent.GetChild(0).GetComponent<ParticleSystem>();
        hitCountText.text = hitCount.ToString();
        _feedbackTextMeshProUGUI=feedbackTextGameObject.GetComponent<TextMeshProUGUI>();
        
        _feedbackTexts.Add("Rate");
        _feedbackTexts.Add("Range");
        _feedbackTexts.Add("Cash!");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            other.GetComponent<Disc>().SendDiscBack();
            Color color = GetComponent<MeshRenderer>().material.color;
            _stoneColor = color;
            var gibletParticleMain = _smokeParticle.main;
            gibletParticleMain.startColor = _stoneColor;
            
            other.GetComponent<Collider>().enabled = false;
            UpdateHitCount();
            if (hitCount==0)
            {
                DetermineUpgradeType();
                feedbackTextGameObject.SetActive(true);
                _feedbackTextMeshProUGUI.text = _feedbackText;
                _smokeParticle.Play();
                mysteryBox.SetActive(false);
                transform.DOScale(Vector3.zero, .35f);
            }
        }
        else if (other.CompareTag("Player"))
        {
            CGameManager.Instance.GameOver();
        }
    }

    void DetermineUpgradeType()
    {
        int randomTextNo = Random.Range(0, 3);
        int randomUpgradeNo=Random.Range(1, 3);
        switch (randomTextNo)
        {
            case 0:
                _feedbackText="+"+randomUpgradeNo+" "+_feedbackTexts[0];
                DiscThrower.Instance.AdjustThrowRate(randomUpgradeNo);
                break;
            case 1:
                _feedbackText="+"+randomUpgradeNo+" "+_feedbackTexts[1];
                DiscThrower.Instance.AdjustThrowRange(randomUpgradeNo);
                break;
            case 2:
                _feedbackText = _feedbackTexts[2];
                dolarBillParticle.Play();
                CGameManager.Instance.OnCashCollected();
                break;
        }
    }

    private void UpdateHitCount()
    {
        hitCount--;
        hitCountText.text = hitCount.ToString();
    }
}
