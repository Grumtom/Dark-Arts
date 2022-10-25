using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class ELGEEBEETEEplayermove : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public float jumpHeight;
    public int jumpNum;
    public int jumps;
    public float maxVertLook;
    private CharacterController characterController;
    public GameObject myCamera;
    private Vector3 playerSchmoover;
    public float gravityValue = -9.8f/60f;
    private float fallspeed = 0;
    public bool grounded;
    public int counterDefault;
    private int counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = gameObject.AddComponent<CharacterController>();
        jumps = jumpNum;
        characterController.skinWidth = 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()

    {
        fallspeed += gravityValue;

        if (characterController.isGrounded)
        {
            fallspeed = -0.01f;
            jumps = jumpNum;
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        Vector3 forewad = transform.forward * Input.GetAxis("Vertical"); // vectors the directional inputs
        Vector3 right = transform.right * Input.GetAxis("Horizontal");

        if (Input.GetAxis("Jump") > 0.1 && jumps > 0 && counter < 1)
        {
            jumps -= 1;
            fallspeed = +jumpHeight;
            counter = counterDefault;
        }
        else
        {
            counter--;
        }

        if (!grounded && jumps == jumpNum)
        {
            jumps = 1;
            
        }
    

    transform.Rotate(0, Input.GetAxis("Mouse X")*turnSpeed, 0);
        myCamera.transform.Rotate(-Input.GetAxis("Mouse Y")*turnSpeed, 0, 0); // rotates the character left and right, then the camera up and down
        
        Vector3 down = transform.up * fallspeed; // vectors the falling
        playerSchmoover = forewad + right;// adds the foreward and right movement to the schmoover vector
        playerSchmoover *= moveSpeed; // FUCKING PENIS COCK FUNNY HAHAHAHAHAHAHAHA, also this multiplies the movement by speed
        playerSchmoover += down; // after the speed is added then the falling is added

        characterController.Move(playerSchmoover);
    }
}
