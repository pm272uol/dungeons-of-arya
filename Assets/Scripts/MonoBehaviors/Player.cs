using System;
using System.Collections;
using UnityEngine;

public class Player : Character
{
    public AudioClip SwordSound; // The sound of the sword

    public AudioClip HealPotion; // The sound of heal potion

    public AudioClip CoinSound; // The sound of coin sound

    public AudioClip FlowerStoneSound; // The sound of flower stone

    public AudioClip ChestSound; // The sound of flower stone

    public AudioSource audioSource;

    public HitPoints hitPoints; // The health points of the character

    public Inventory inventoryPrefab;

    Inventory inventory;

    public HealthBar healthBarPrefab;

    HealthBar healthbar;

    public float meleeRange = 1f; // The range of Melee Attack
    public int meleeDamage = 5; // The damage of Melee Attack

    private Animator animator;

    public event Action OnPlayerDeath; // New event, when the player dies, this will trigger

    public bool unbeatable = false;


    private void OnEnable()
    {
        ResetCharacter();
    }

    private void Start()
    {

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>(); // Get the audio source

        if (musicManager == null)
        {
            musicManager = FindObjectOfType<MusicManager>(); 
            if (musicManager == null)
            {
                Debug.LogWarning("No MusicManager found in the scene.");
            }
        }
    }

    private void Update()
    {
        // Melee Attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        // Play the sound of sword sound
        audioSource.PlayOneShot(SwordSound); 

        // find the closet enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, meleeRange);
        Collider2D closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("EmenyObject")) // Get all the enemy
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance && distance < meleeRange * 3)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null) // If there is a closet enemy
        {
            // Calculate the attack direction
            Vector2 attackDirection = closestEnemy.transform.position - transform.position;
            attackDirection.Normalize();

            // Set the animation of attack
            if( animator != null)
            {
                animator.SetFloat("attackXDir", attackDirection.x);
                animator.SetFloat("attackYDir", attackDirection.y);
                animator.SetTrigger("MeleeAttack");
            }

            // Damage the enemy
            Enemy enemyComponent = closestEnemy.GetComponent<Enemy>();
            StartCoroutine(enemyComponent.DamageCharacter(meleeDamage, 0));

        } else // If the is not an enemy
        {
            // Set the animation of attack south
            if (animator != null)
            {
                animator.SetFloat("attackXDir", 0.0f);
                animator.SetFloat("attackYDir", -1.0f);
                animator.SetTrigger("MeleeAttack");
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CanBePickedUp"))
        {

            Item hitObject = collision.gameObject.GetComponent<Consumable>().item; // Get the item of the consumable if the player hit the object

            if(hitObject != null)
            {
                bool shouldDisappear = false;

                print("This is:" + hitObject.objectName); // Print the name of the object
                
                switch(hitObject.itemType)
                {
                    case Item.ItemType.COIN: // If the object is a coin

                        // Play the sound of coins
                        audioSource.PlayOneShot(CoinSound);

                        shouldDisappear = inventory.AddItem(hitObject);

                        shouldDisappear = true;
                        break;

                    case Item.ItemType.HEALTH: // If the object is a health

                        // Play the sound of heal potion
                        audioSource.PlayOneShot(HealPotion);

                        shouldDisappear = AdjustHitPoints(hitObject.quantity);

                        break;

                    case Item.ItemType.FLOWERSTONE1: // If the object is a flowerstone1

                        // Play the sound of flowerstone
                        audioSource.PlayOneShot(FlowerStoneSound);


                        shouldDisappear = inventory.AddItem(hitObject);

                        shouldDisappear = true;
                        break;

                    case Item.ItemType.FLOWERSTONE2: // If the object is a flowerstone2


                        // Play the sound of flowerstone
                        audioSource.PlayOneShot(FlowerStoneSound);

                        shouldDisappear = inventory.AddItem(hitObject);

                        shouldDisappear = true;
                        break;

                    case Item.ItemType.FLOWERSTONE3: // If the object is a flowerstone3

                        // Play the sound of flowerstone
                        audioSource.PlayOneShot(FlowerStoneSound);

                        shouldDisappear = inventory.AddItem(hitObject);

                        shouldDisappear = true;
                        break;

                    case Item.ItemType.FLOWERSTONE4: // If the object is a flowerstone4

                        // Play the sound of flowerstone
                        audioSource.PlayOneShot(FlowerStoneSound);

                        shouldDisappear = inventory.AddItem(hitObject);

                        shouldDisappear = true;
                        break;

                    case Item.ItemType.KEY: // If the object is a key

                        // Play the sound of coins
                        audioSource.PlayOneShot(CoinSound);

                        shouldDisappear = inventory.AddItem(hitObject);

                        shouldDisappear = true;
                        break;

                    case Item.ItemType.CHEST: // If the object is a chest

                        // Jump to the Game ending page

                        // Play the sound of chest
                        audioSource.PlayOneShot(ChestSound);

                        shouldDisappear = true;
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

    public override IEnumerator DamageCharacter(int damage, float interval) // Damage the player
    {
        while (true)
        {

            StartCoroutine(FlickerCharacter()); // Flicker the character

            if (!unbeatable)
            {
                hitPoints.value = hitPoints.value - damage;
            }
            

            if (hitPoints.value <= float.Epsilon)
            {

                KillCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public override void KillCharacter()
    {
        OnPlayerDeath?.Invoke();

        base.KillCharacter();

        Destroy(healthbar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override void ResetCharacter()
    {
        inventory = Instantiate(inventoryPrefab);

        healthbar = Instantiate(healthBarPrefab);

        healthbar.character = this;

        hitPoints.value = startingHitPoints;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }

}
