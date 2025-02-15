using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public float maxHitPoints; // The max health points of the character

    public float startingHitPoints; // The starting hit points of the character

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    public abstract void ResetCharacter();

    public abstract IEnumerator DamageCharacter(int damage, float interval);
}
