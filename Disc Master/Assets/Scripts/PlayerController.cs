using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float sideSpeed = 10f;


    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * sideSpeed * horizontalInput);
        
        LimitX();
    }
    
    //Create a method to limit movement on the x axis
    private void LimitX()
    {
        //if the player's x position is greater than 10
        if (transform.position.x > 10)
        {
            //set the player's x position to 10
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
        //if the player's x position is less than -10
        else if (transform.position.x < -10)
        {
            //set the player's x position to -10
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }
    }
}
