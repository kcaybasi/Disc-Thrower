using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

public class Disc : MonoBehaviour
{
    private float _timer;
    [SerializeField] private float lifeTime=1f;


    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer>=lifeTime)
        {
            _timer = 0;
            ObjectPooler.Instance.DiscPool.Release(this.gameObject);
            DOTween.Kill(transform);
        }
    }
}
