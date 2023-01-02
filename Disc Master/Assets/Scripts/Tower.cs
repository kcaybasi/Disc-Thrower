using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<Stone> _stones;
    private int _hitCountForReward;
    [SerializeField] private List<GameObject> cashObjects = new List<GameObject>();

    private void Start()
    {
        GetHitNeededHitCount();
    }

    void GetHitNeededHitCount()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Stone>())
            {
                Stone stone = child.GetComponent<Stone>();
                _hitCountForReward += stone.HitCount;
            }
        }
    }
    
}
