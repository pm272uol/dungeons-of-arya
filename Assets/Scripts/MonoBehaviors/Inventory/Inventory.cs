using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab; // The prefab of slots

    public const int numSlots = 5; // The number of slots 

    Image[] itemImages = new Image[numSlots]; // The number of images
    Item[] items = new Item[numSlots]; // The items
    GameObject[] slots = new GameObject[numSlots]; // The slots

    public void Start()
    {
        CreateSlots(); // Create slots
    }

    public void CreateSlots()
    {
        if (slotPrefab != null)
        {
            for (int i = 0; i < numSlots; i++)
            {
                GameObject newSlot = Instantiate(slotPrefab); // create a new slot from prefab

                newSlot.name = "ItemSlot_" + i; // The slot name

                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform); // Set the parent of the slot

                slots[i] = newSlot; // Set the array of the slot 

                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>(); // Set the image of the slot
            }
        }
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.stackable == true)
            { // If the slot already has item
                
                items[i].quantity = items[i].quantity + 1; // Add the quantity

                Slot slotScript = slots[i].GetComponent<Slot>(); // Add the slot

                Text quantityText = slotScript.qtyText; // Get the quantity text

                quantityText.enabled = true; // Enable quantity

                quantityText.text = items[i].quantity.ToString(); // Enable text of quantity
                return true;
            }

            if (items[i] == null)
            {
                // Adding to empty slot
                // Copy item and add to inventory. Copying so we dont modify original Scriptable Object
                items[i] = Instantiate(itemToAdd); // Create the item

                items[i].quantity = 1; // Create the quantity

                itemImages[i].sprite = itemToAdd.sprite; // Add the sprite

                itemImages[i].enabled = true; // Enable the image

                return true;
            }
        }
        return false;
    }
}