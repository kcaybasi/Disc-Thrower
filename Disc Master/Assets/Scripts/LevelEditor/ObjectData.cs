using UnityEngine;
using System;
namespace LevelEditor
{
    
    [Serializable]
    public class ObjectData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public GameObject Prefab;
    }
}
