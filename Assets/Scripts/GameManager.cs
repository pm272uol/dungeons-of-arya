using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraManager cameraManager;

    public SpawnPoint playerSpawnPoint;

    public SpawnPoint enemySpawnPoint;

    //public static GameManager Instance { get; private set; }
    public static GameManager Instance = null;

    private int torchesLit = 0;
    public int requiredTorches = 4;
    public GameObject treasureChest; // Assign in Inspector

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

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
        SpawnEnemy();
    }

    public void SpawnPlayer()
    {
        if(playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }

    public void SpawnEnemy()
    {
        if (enemySpawnPoint != null)
        {
            GameObject enemy = enemySpawnPoint.SpawnObject();
        }
    }
}

