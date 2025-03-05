using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "item")]

public class Item : ScriptableObject // The items of the player
{
    public string objectName;

    public Sprite sprite;

    public int quantity;

    public bool stackable;

    public enum ItemType
    {
        COIN,
        HEALTH,
        FLOWERSTONE1,
        FLOWERSTONE2,
        FLOWERSTONE3,
        FLOWERSTONE4,
        KEY,
        CHEST
    }

    public ItemType itemType;
}
