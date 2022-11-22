using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            transform.DOScale(Vector3.zero, .2f);
            other.GetComponent<Collider>().enabled = false;
        }
    }
}
