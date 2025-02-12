using UnityEngine;

public class Player : Character
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CanBePickedUp"))
        {

            Item hitObject = collision.gameObject.GetComponent<Consumable>().item; // Get the item of the consumable if the player hit the object

            if(hitObject != null)
            {

                print("Hit:" + hitObject.objectName); // Print the name of the object
                
                switch(hitObject.itemType)
                {
                    case Item.ItemType.COIN: // If the object is a coin
                        break;

                    case Item.ItemType.HEALTH: // If the object is a health
                        AdjustHitPoints(hitObject.quantity);
                        break;

                    default:
                        break;

                }
                collision.gameObject.SetActive(false);
            }

        }
    }

    public void AdjustHitPoints(int amount) // Adjust the health of the player
    {
        hitPoints = hitPoints + amount;

        print("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints);
    }

}
