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

    public void Tutorial()
    {
        SceneManager.LoadScene("tutorial");
    }

    private void OnTriggerEnter(Collider other)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (other.gameObject.tag == "Player" && winbox)
        {
            print("Win");
            if (sceneName == "Tutorial")
            {
                SceneManager.LoadScene("Level 1");
            }
            else if (sceneName == "Level 1")
            {
                SceneManager.LoadScene("Main Menu");
            }

        }
    }
    
}
