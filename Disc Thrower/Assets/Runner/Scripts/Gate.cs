using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing a Spawnable object.
    /// If a GameObject tagged "Player" collides
    /// with this object, it will trigger a fail
    /// state with the GameManager.
    /// </summary>
    public class Gate : MonoBehaviour
    {
        const string k_PlayerTag = "Player";

        [SerializeField]
        GateType m_GateType;
        [SerializeField]
        float m_Value;
        [SerializeField]
        RectTransform m_Text;

        bool m_Applied;
        Vector3 m_TextInitialScale;

        private void Awake()
        {
            string signText = null;
            switch (Mathf.Sign(m_Value))
            {
                case 1: signText = "+"+m_Value;
                    break;
                case -1: signText = m_Value.ToString(CultureInfo.CurrentCulture);
                    break;
            }
            m_Text.GetComponent<TextMeshPro>().text = signText;
        }

        enum GateType
        {
            ChangeRate,
            ChangeRange,
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
                case GateType.ChangeRate:
                    DiscThrower.Instance.AdjustThrowRate(m_Value);
                break;

                case GateType.ChangeRange:
                    DiscThrower.Instance.AdjustThrowRange(m_Value);
                break;
            }

            m_Applied = true;
        }
    }
}