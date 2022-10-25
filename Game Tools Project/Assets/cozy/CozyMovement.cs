using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CozyMovement : MonoBehaviour
 
 {
     public CharacterController myCharacterController;
     public float turnSpeed;
     public float moveSpeed;
     public float depraj = 0;
 
     // Start is called before the first frame update
     void Start()
     {
         
     }
 
     // Update is called once per frame
     void Update()
     {
         Vector3 right = transform.up * Input.GetAxis("Horizontal") * turnSpeed;
         Vector3 forewad = transform.forward * Input.GetAxis("Vertical") * moveSpeed/100; // vectors the directional inputs
         myCharacterController.Move(forewad);
         if(Input.GetAxis("Horizontal")!=0){gameObject.transform.Rotate(right);}
     }
 }
