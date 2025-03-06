using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_UI : MonoBehaviour
{
    [SerializeField] private string gameScene = "Main";
    [SerializeField] private string tutorialScene = "Tutorial";
    [SerializeField] private string menuScene = "StartMenu";
    
    public void StartGameButton()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene(tutorialScene);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(menuScene);
    }
}
