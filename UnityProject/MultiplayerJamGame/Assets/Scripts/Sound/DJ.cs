using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DJ : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0))
        {
            GetComponent<AudioManager>().Play("MainMenu");
        } else if(SceneManager.GetActiveScene() == SceneManager.GetSceneAt(1))
        {
            GetComponent<AudioManager>().Play("MainMenu") ;
        }
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneAt(3))
        {
            GetComponent<AudioManager>().Play("Level");
        }
    }
}
