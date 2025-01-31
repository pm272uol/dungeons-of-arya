using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<Torch> torches; 
    public Door door;
    private bool roomCompleted = false;

    void Start()
    {
        // Assign this RoomManager to each torch.
        foreach (Torch torch in torches)
        {
            torch.SetRoomManager(this); 
        }
    }

    public void TorchLit()
    {
        // Check if all torches are lit.
        if (!roomCompleted && torches.TrueForAll(torch => torch.IsLit()))
        {
            roomCompleted = true;
            door.OpenDoor();
        }
    }

    public bool IsRoomComplete()
    {
        return roomCompleted;
    }
}
