using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Disc : MonoBehaviour
{
    private float _timer;
    [SerializeField] private float lifeTime=3f;
    
    private void Start()
    { 
        
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer>=lifeTime)
        {
            _timer = 0f;
            ObjectPooler.Instance.DiscPool.Release(this.gameObject);
        }
    }
}
