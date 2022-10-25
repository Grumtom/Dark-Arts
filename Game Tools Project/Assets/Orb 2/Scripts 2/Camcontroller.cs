using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camcontroller : MonoBehaviour
{
    public Transform player;
    public float camHeight;
    public float camDist;

    // Update is called once per frame
    void Update () {
        transform.position = player.transform.position + new Vector3(0, camHeight, camDist);
    }
}
