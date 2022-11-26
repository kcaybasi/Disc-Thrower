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
    private ThrowType _throwType=ThrowType.RegularThrow;

    public enum ThrowType
    {
        RegularThrow,
        DualShot,
        SpreadShot,
    }
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

    public void ChangeThrowType(ThrowType throwType)
    {
        _throwType = throwType;
    }

    private void Update()
    {
        if (_isThrowAllowed)
        {
            StartCoroutine(DiscThrowRoutine());
        }
    }

    void GetThrowType()
    {
        switch (_throwType)
        {
            case ThrowType.RegularThrow:
                ThrowDisc();
                break;
            case ThrowType.DualShot:
                DualThrow();
                break;
            case ThrowType.SpreadShot:
                ThrowDisc();
                break;
        }
    }
    void ThrowDisc()
    {
        GameObject obj = ObjectPooler.Instance.DiscPool.Get();
        
        obj.transform.position = transform.position+Vector3.up*.2f;
        
        float currentZ = obj.transform.position.z;
        obj.transform.DOMoveZ(currentZ + _baseRange+DiscThrowRange, 5f);
    }

    void DualThrow()
    {
        GameObject obj = ObjectPooler.Instance.DiscPool.Get();
        GameObject obj_2 = ObjectPooler.Instance.DiscPool.Get();
        
        obj.transform.position = transform.position+Vector3.up*.2f-Vector3.left*.5f;
        obj_2.transform.position = transform.position+Vector3.up*.2f-Vector3.right*.5f;
        
        float currentZ = obj.transform.position.z;
        float currentZ_2 = obj_2.transform.position.z;
        obj.transform.DOMoveZ(currentZ + _baseRange+DiscThrowRange, 5f);
        obj_2.transform.DOMoveZ(currentZ + _baseRange+DiscThrowRange, 5f);
    }

    IEnumerator DiscThrowRoutine()
    {
        _isThrowAllowed = false;
        GetThrowType();
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
