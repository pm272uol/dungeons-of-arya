using UnityEngine;

public class Player : Character
{

    public HealthBar healthBarPrefab;

    HealthBar healthbar;

    private void Start()
    {
        hitPoints.value = startingHitPoints;

        healthbar = Instantiate(healthBarPrefab);

        healthbar.character = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CanBePickedUp"))
        {

            Item hitObject = collision.gameObject.GetComponent<Consumable>().item; // Get the item of the consumable if the player hit the object

            if(hitObject != null)
            {
                bool shouldDisappear = false;

                print("Hit:" + hitObject.objectName); // Print the name of the object
                
                switch(hitObject.itemType)
                {
                    case Item.ItemType.COIN: // If the object is a coin

                        shouldDisappear = true;
                        break;

                    case Item.ItemType.HEALTH: // If the object is a health

                        shouldDisappear = AdjustHitPoints(hitObject.quantity);

                        break;

                    default:
                        break;

                }

                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
                
            }

        }
    }

    public bool AdjustHitPoints(int amount) // Adjust the health of the player
    {
        if(hitPoints.value < maxHitPoints) // If the player does not have max hit points
        {
            hitPoints.value = hitPoints.value + amount;

            print("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints.value);

            return true;
        }

        return false; // if the player already has maxHitPoints

        
    }

}
