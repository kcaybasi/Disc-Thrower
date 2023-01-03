using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusCubeManager : MonoBehaviour
{
    [SerializeField] private GameObject bonusCubePrefab;
    private GameObject _bonusCube;
    private GameObject _base;
    MeshRenderer _bonusCubeMeshRenderer;
    MeshRenderer _baseMeshRenderer;
    private float _deltaZ;
    private int _hitCount=2;
    private float _colorConstant;
    private void Awake()
    {
        for (int i = 0; i < 25; i++)
        {
            var cube = SpawnCube();
            SetCubeColor(cube);
            SetCubeHitCount();
        }
    }

    private GameObject SpawnCube()
    {
        GameObject cube = Instantiate(bonusCubePrefab, transform);
        cube.transform.position = new Vector3(0, 2.9f, 151.232f + _deltaZ);
        _deltaZ += (157.5498f - 151.232f);
        return cube;
    }

    private void SetCubeHitCount()
    {
        _bonusCube.GetComponent<BonusCube>().HitCount = _hitCount;
        _bonusCube.GetComponent<BonusCube>().hitCountText.text = _hitCount.ToString();
        _hitCount++;
    }

    private void SetCubeColor(GameObject cube)
    {
        _base= cube.transform.GetChild(2).gameObject;
        _bonusCube = cube.transform.GetChild(1).gameObject;
        _bonusCubeMeshRenderer = _bonusCube.GetComponent<MeshRenderer>();
        _baseMeshRenderer= _base.GetComponent<MeshRenderer>();
        _bonusCubeMeshRenderer.material.color = Color.HSVToRGB(_colorConstant, 1f, 1f);
        _baseMeshRenderer.material.color = Color.HSVToRGB(_colorConstant, 1f, 1f);
        if (_colorConstant > 1)
            _colorConstant = 0;
        else
            _colorConstant += 0.06f;
    }
}
