using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingIcon : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 0f, -40f * Time.deltaTime);
    }
}
