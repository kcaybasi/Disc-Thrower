using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NUnit.Framework.Constraints;

public class DiscThrower : MonoBehaviour
{
    public static DiscThrower Instance => sDiscThrower;
    static DiscThrower sDiscThrower;
    
    private bool _isThrowAllowed=true;
    private GameObject _discObj;
    private float _baseRange;
    private float _baseRate;
    public float DiscThrowRange { get; set; }
    public float DiscThrowRate { get; set; }

    private void Awake()
    {
        if (sDiscThrower != null && sDiscThrower!= this)
        {
            Destroy(gameObject);
            return;
        }
        sDiscThrower= this;
        
        _baseRange = 35f;
        _baseRate = .35f;
    }

    private void Update()
    {
        if (_isThrowAllowed)
        {
            StartCoroutine(DiscThrowRoutine());
        }
    }

    void ThrowDisc()
    {
        GameObject obj = ObjectPooler.Instance.DiscPool.Get();
        
        obj.transform.position = transform.position+Vector3.up*.2f;
        
        float currentZ = obj.transform.position.z;
        obj.transform.DOMoveZ(currentZ + _baseRange+DiscThrowRange, 5f);
    }

    IEnumerator DiscThrowRoutine()
    {
        _isThrowAllowed = false;
        ThrowDisc();
        yield return new WaitForSeconds(_baseRate-DiscThrowRate);
        _isThrowAllowed = true;
    }

    public void AdjustThrowRate(float value)
    {
        DiscThrowRate += value*.1f;
    }

    public void AdjustThrowRange(float value)
    {
        DiscThrowRange += value;
    }
    
    
    
}
