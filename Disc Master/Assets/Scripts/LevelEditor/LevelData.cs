using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelEditor
{
    [Serializable]
    public class LevelData : ScriptableObject
    {
        public GameObject  spawnablePrefab;
        public List<ObjectData> LevelObjects = new List<ObjectData>();
    }
}
