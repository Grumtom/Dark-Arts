using System;
using System.Collections;
using System.Collections.Generic;
using Orb_2.Scripts_2;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool menu;
    public GameObject menuScreen;
    public GameObject player;
    public P2Player playerScript;

    private void Awake()
    {
        playerScript = player.GetComponent<P2Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuSwitch();
        }
    }

    public void menuSwitch()
    {
        print("menu");
        if (!menu)
        {
            //turn menu on
            menu = true;
            playerScript.active = false;
            Time.timeScale = 0;
            menuScreen.SetActive(true);
        }
        else
        {
            // turns menu off
            Time.timeScale = 1;
            menu = false;
            playerScript.active = true;
            menuScreen.SetActive(false);
        }
    }
}
