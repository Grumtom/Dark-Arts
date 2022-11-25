using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d") ||
            Input.GetKeyDown("r") || Input.GetMouseButtonDown(1)) 
        {
            Destroy(gameObject); // destroys the mana with input
        }
    }
}
