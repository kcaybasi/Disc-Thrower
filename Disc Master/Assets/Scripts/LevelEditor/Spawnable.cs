using System;
using UnityEngine;

namespace LevelEditor
{
    [Serializable]
    public class Spawnable : MonoBehaviour
    {
        public GameObject Prefab { get; set; }
    }
}
