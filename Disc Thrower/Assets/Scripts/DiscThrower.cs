using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiscThrower : MonoBehaviour
{
    private bool _isThrowAllowed=true;
    private GameObject _discObj;
    void ThrowDisc()
    {
        GameObject obj = ObjectPooler.Instance.DiscPool.Get();
        
        obj.transform.position = transform.position+Vector3.up*.25f;
        
        float currentZ = obj.transform.position.z;
        obj.transform.DOMoveZ(currentZ + 50f, 5f);
       
    }
    
    private void Update()
    {
        if (_isThrowAllowed)
        {
            StartCoroutine(DiscThrowRoutine());
        }
    }

    IEnumerator DiscThrowRoutine()
    {
        _isThrowAllowed = false;
        ThrowDisc();
        yield return new WaitForSeconds(.35f);
        _isThrowAllowed = true;
    }
}
