using System;
using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public float maxHitPoints; // The max health points of the character

    public float startingHitPoints; // The starting hit points of the character

    public bool isDead = false; // Check if the character is dead

    [SerializeField] protected MusicManager musicManager; // Music Manager to play the music of getting damaged

    

    public virtual void KillCharacter()
    {

        //Destroy(gameObject);
        isDead = true;
        gameObject.SetActive(false); // handle death logic, such as hiding the character

    }


    public abstract void ResetCharacter();

    public abstract IEnumerator DamageCharacter(int damage, float interval);

    public virtual IEnumerator FlickerCharacter()
    {
        Debug.Log("Flicker the player");

        if (musicManager != null)
        {
            musicManager.PlayDamageMusic();
        }

        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
