using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cash : MonoBehaviour
{
    const string k_PlayerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(k_PlayerTag))
        {
            transform.DOMove(other.transform.position, .2f);
            transform.DOScale(Vector3.zero, .1f);
        }
    }
}
