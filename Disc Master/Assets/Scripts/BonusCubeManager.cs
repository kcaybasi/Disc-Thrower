using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCubeManager : MonoBehaviour
{
    [SerializeField] private GameObject bonusCubePrefab;
    private float _deltaZ;

    private void Start()
    {
        //Spawn 10 cubes
        for (int i = 0; i < 10; i++)
        {
            //Spawn a cube
            GameObject cube = Instantiate(bonusCubePrefab, transform);
            //Set the position of the cube
            cube.transform.position = new Vector3(0, 2.9f, 151.232f+_deltaZ);
            _deltaZ += (157.5498f - 151.232f);
        }
    }
}
