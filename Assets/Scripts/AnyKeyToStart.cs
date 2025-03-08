using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyToStart : MonoBehaviour
{
    // Set this to the scene you want to load
    [SerializeField] private string sceneToLoad = "Main";

    void Update()
    {
        if (Input.anyKeyDown) // Detects if any key is pressed down
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

