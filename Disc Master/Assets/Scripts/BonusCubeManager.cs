using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusCubeManager : MonoBehaviour
{
    [SerializeField] private GameObject bonusCubePrefab;
    GameObject _bonusCube;
    MeshRenderer _bonusCubeMeshRenderer;
    private float _deltaZ;
    private int _hitCount;
    private void Awake()
    {
        _hitCount = 2;
        //Spawn 25 cubes
        for (int i = 0; i < 25; i++)
        {
            //Spawn a cube
            GameObject cube = Instantiate(bonusCubePrefab, transform);
            //Set the position of the cube
            cube.transform.position = new Vector3(0, 2.9f, 151.232f+_deltaZ);
            _deltaZ += (157.5498f - 151.232f);
            //Set every color in random hue pattern
            _bonusCube = cube.transform.GetChild(1).gameObject;
            _bonusCubeMeshRenderer = _bonusCube.GetComponent<MeshRenderer>();
            _bonusCubeMeshRenderer.material.color = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);
            //Set hit counts 
            _bonusCube.GetComponent<BonusCube>().HitCount = _hitCount;
            _bonusCube.GetComponent<BonusCube>().hitCountText.text = _hitCount.ToString();
            _hitCount++;
        }
    }
}
