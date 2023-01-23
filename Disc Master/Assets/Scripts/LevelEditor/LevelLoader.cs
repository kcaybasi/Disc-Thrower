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
        private LevelData _currentLevelData;
        public GameObject[] levelObjects;
        private void Awake()
        {
            //Singleton
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void SpawnLevelObjects(int levelNumber )
        {
            _currentLevelData= levelDataList[levelNumber-1];
            levelObjects = new GameObject[_currentLevelData.LevelObjects.Count];
            for (int i = 0; i < _currentLevelData.LevelObjects.Count; i++)
            {
                levelObjects[i] = Instantiate(_currentLevelData.LevelObjects[i].Prefab, _currentLevelData.LevelObjects[i].Position,
                    _currentLevelData.LevelObjects[i].Rotation);
            }
        }
    }
}
