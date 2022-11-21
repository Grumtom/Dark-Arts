using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour
{
    public GameObject Camera;
    public float distance;

    private void Awake()
    {
        Camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Camera.transform.rotation;  // holds the healthbar in one position
    }
}
