using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class LevelLoader : MonoBehaviour
    {
        //Singleton
        public static LevelLoader Instance;
        
        [Header("Level Data")]
        [SerializeField] List<LevelData> levelDataList;

        private LevelData _levelData;
        public GameObject[] levelObjects;
        private void Awake()
        {
            //Singleton
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            SpawnLevelObjects();
        }

        private void SpawnLevelObjects()
        {
            levelObjects = new GameObject[_levelData.LevelObjects.Count];
            for (int i = 0; i < _levelData.LevelObjects.Count; i++)
            {
                levelObjects[i] = Instantiate(_levelData.LevelObjects[i].Prefab, _levelData.LevelObjects[i].Position,
                    _levelData.LevelObjects[i].Rotation);
            }
        }

        public void LoadNextLevel()
        {
            
        }
    }
}
