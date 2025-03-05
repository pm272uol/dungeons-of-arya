using System.Collections.Generic;
using UnityEngine;

public class RoomManager2 : MonoBehaviour
{
    // List to hold all enemies in the room
    public List<Enemy> enemies;

    // Reference to the door that will be opened when all enemies are defeated
    public Door door;

    // Flag to check if the room has been completed
    private bool roomCompleted = false;

    [SerializeField] GameObject FlowerStone; // The flowerstone of this room

    void Start()
    {
        // Initialization logic can be placed here if needed
        if (FlowerStone != null)
        {
            FlowerStone.SetActive(false);
        }
        
    }

    void Update()
    {
        // Check the status of enemies each frame
        CheckEnemiesStatus();
    }

    private void CheckEnemiesStatus()
    {
        // Check if all enemies in the list are dead
        if (!roomCompleted && enemies.TrueForAll(enemy => enemy.isDead))
        {
            roomCompleted = true; // Mark the room as completed
            door.OpenDoor(); // Open the door when all enemies are defeated

            if(FlowerStone != null)
            {
                FlowerStone.SetActive(true);
            }
            
        }
    }

    // Method to check if the room is complete
    public bool IsRoomComplete()
    {
        return roomCompleted;
    }
}