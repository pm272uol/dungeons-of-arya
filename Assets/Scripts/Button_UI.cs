using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_UI : MonoBehaviour
{
    [SerializeField] private string newLevel = "TestMap - Bill";
    
    public void StartGameButton()
    {
        SceneManager.LoadScene(newLevel);
    }

}
