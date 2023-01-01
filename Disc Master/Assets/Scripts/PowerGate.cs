using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGate : MonoBehaviour
{  
    const string k_PlayerTag = "Player";
    [SerializeField]
    GateType m_GateType;
    bool m_Applied;
    
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    enum GateType
    {
        DualShot,
        SpreadShot,
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(k_PlayerTag) && !m_Applied)
        {
            ActivateGate();
        }
    }

    void ActivateGate()
    {
        switch (m_GateType)
        {
            case GateType.DualShot:
                DiscThrower.Instance.ChangeThrowType(DiscThrower.ThrowType.DualShot);
                break;
            case GateType.SpreadShot:
                DiscThrower.Instance.ChangeThrowType(DiscThrower.ThrowType.SpreadShot);
                break;
        }
        _meshRenderer.material.color = Color.grey;
        m_Applied = true;
    }
}
