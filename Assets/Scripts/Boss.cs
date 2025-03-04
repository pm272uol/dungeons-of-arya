using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    float hitPoints;

    public int damageStrength;

    Coroutine damageCoroutine;

    Animator animator;

    public bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override IEnumerator DamageCharacter(int damage, float interval) // Damage the enemy
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter()); // Flicker the character

            hitPoints = hitPoints - damage;

            if(hitPoints <= float.Epsilon){
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

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    public void OnEnable()
    {
        ResetCharacter();
    }

    void OnCollisionEnter2D(Collision2D collision) // If the enemy is having a collision with the player
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            animator.SetBool("Attack", true); // Set the animation of attack

            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f)); // Damage the player with damageStrength
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            animator.SetBool("Attack", false); // finish the animation of attack

            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
}
