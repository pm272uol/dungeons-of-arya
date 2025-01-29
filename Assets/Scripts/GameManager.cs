using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private int torchesLit = 0;
    public int requiredTorches = 4;
    public GameObject treasureChest; // Assign in Inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
}

