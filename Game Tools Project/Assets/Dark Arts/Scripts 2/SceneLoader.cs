using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private SceneManager Manager;
    public bool winbox;
    
    
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void level_1()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Pracitce()
    {
        SceneManager.LoadScene("Practice Arena");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && winbox)
        {
       
            SceneManager.LoadScene("Main Menu");
        }
    }
    
}
