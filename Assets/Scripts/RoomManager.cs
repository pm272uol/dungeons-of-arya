using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<Torch> torches; 
    public Door door;
    private bool roomCompleted = false;
    [SerializeField] GameObject FlowerStone;

    void Start()
    {
        // Assign this RoomManager to each torch.
        foreach (Torch torch in torches)
        {
            torch.SetRoomManager(this); 
        }

        FlowerStone.SetActive(false);
    }

    public void TorchLit()
    {
        // Check if all torches are lit.
        if (!roomCompleted && torches.TrueForAll(torch => torch.IsLit()))
        {
            roomCompleted = true;
            door.OpenDoor();

            FlowerStone.SetActive(true); // Set the flowerstone to be active
        }
    }

    public bool IsRoomComplete()
    {
        return roomCompleted;
    }


}
