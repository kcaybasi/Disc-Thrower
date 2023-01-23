using System;
using UnityEngine;

namespace LevelEditor
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] LevelData levelData;
        public GameObject[] levelObjects;

        private void Start()
        {
            levelObjects = new GameObject[levelData.LevelObjects.Count];
            for (int i = 0; i < levelData.LevelObjects.Count; i++)
            {
                levelObjects[i] = Instantiate(levelData.LevelObjects[i].Prefab, levelData.LevelObjects[i].Position, levelData.LevelObjects[i].Rotation);
            }
        }
    }
}
