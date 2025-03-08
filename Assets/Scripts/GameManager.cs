using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CameraManager cameraManager;

    public SpawnPoint playerSpawnPoint;

    //public SpawnPoint enemySpawnPoint;

    //public static GameManager Instance { get; private set; }
    public static GameManager Instance = null;

    private int torchesLit = 0;
    public int requiredTorches = 4;
    public GameObject treasureChest; // Assign in Inspector

    public GameObject pauseMenu;
    private bool isPaused = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);    // Show the pause menu
        Time.timeScale = 0f;            // Freeze the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);   // Hide the pause menu
        Time.timeScale = 1f;            // Resume the game
        isPaused = false;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LightTorch()
    {
        torchesLit++;
        Debug.Log("Torches Lit: " + torchesLit);

        if (torchesLit >= requiredTorches)
        {
            RevealTreasure();
        }
    }

    private void RevealTreasure()
    {
        if (treasureChest != null)
        {
            treasureChest.SetActive(true); // Make chest appear
            Debug.Log("Treasure Revealed!");
        }
    }

    void Start()
    {
        SetupScene();
    }

    public void SetupScene()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if(playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
}
