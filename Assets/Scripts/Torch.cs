﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private bool isLit = false;
    private RoomManager roomManager;

    public void SetRoomManager(RoomManager manager)
    {
        roomManager = manager;
    }

    public void LightTorch()
    {
        if (!isLit)
        {
            isLit = true;
            roomManager.TorchLit();
            // Enable animator on torch game component.
            Animator animator = GetComponent<Animator>();
            animator.enabled = true;                    
        }
    }

    public bool IsLit()
    {
        return isLit;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player interacts with the torch
        {
            LightTorch();
        }
    }
}
