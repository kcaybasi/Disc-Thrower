using UnityEngine;
namespace LevelEditor
{
    [System.Serializable]
    public class ObjectData
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public GameObject Prefab { get; set; }
    }
}
