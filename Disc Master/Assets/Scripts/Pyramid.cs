using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;


public class Pyramid : MonoBehaviour
{
    private ParticleSystem _smokeParticle;
    private Color _stoneColor;
    [SerializeField] private int hitCount;
    public int HitCount => hitCount;
    [SerializeField] private TextMeshProUGUI hitCountText;
    [SerializeField] private GameObject mysteryBox;
    [SerializeField] private GameObject feedbackTextGameObject;
    private TextMeshProUGUI _feedbackTextMeshProUGUI;
    string _feedbackText;
    List<string> _feedbackTexts = new List<string>();


    private void Awake()
    {
        _smokeParticle = transform.parent.GetChild(0).GetComponent<ParticleSystem>();
        _stoneColor = GetComponent<MeshRenderer>().material.color;
        hitCountText.text = hitCount.ToString();
        _feedbackTextMeshProUGUI=feedbackTextGameObject.GetComponent<TextMeshProUGUI>();
        
        _feedbackTexts.Add("Rate");
        _feedbackTexts.Add("Range");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            other.GetComponent<Disc>().SendDiscBack();
            
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
        int randomTextNo = Random.Range(0, 2);
        int randomUpgradeNo=Random.Range(0, 3);
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
        }
    }

    private void UpdateHitCount()
    {
        hitCount--;
        hitCountText.text = hitCount.ToString();
    }
}
