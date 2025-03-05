using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    float hitPoints;

    public int damageStrength;

    Coroutine damageCoroutine;

    Animator animator;

    public event Action<bool> OnDeath; //Event: true for Boss death, false for normal enemy

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
                // Trigger the death event
                OnDeath?.Invoke(AnimatorHasParameter(GetComponent<Animator>(), "BossAttack1")); // Justify if this is a boss

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

           


            if (AnimatorHasParameter(animator, "BossAttack1")) // If the animator is a boss (has boss attack method)
            {
                int randomAttackMethod = UnityEngine.Random.Range(1, 4);

                if (randomAttackMethod == 1)
                {
                    if(musicManager != null)
                    {
                        musicManager.PlayBossAttack1(); // Play the sound of boss attack 1
                    }

                    animator.SetBool("BossAttack1", true); // Set the method of attack to be method 1

                    if (damageCoroutine == null)
                    {
                        damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f)); // Damage the player with damageStrength
                    }
                }
                else if (randomAttackMethod == 2)
                {
                    if(musicManager != null)
                    {
                        musicManager.PlayBossAttack2(); // Play the sound of boss attack 1
                    }

                    animator.SetBool("BossAttack2", true); // Set the method of attack to be method 2

                    if (damageCoroutine == null)
                    {
                        damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength * 2, 1.0f)); // Damage the player with damageStrength * 2
                    }
                }
                else if (randomAttackMethod == 3)
                {
                    if(musicManager != null)
                    {
                        musicManager.PlayBossAttack3(); // Play the sound of boss attack 3
                    }     

                    animator.SetBool("BossAttack3", true); // Set the method of attack to be method 2

                    if (damageCoroutine == null)
                    {
                        damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength * 3, 1.0f)); // Damage the player with damageStrength * 3
                    }
                }
            } 
            else
            {
                musicManager.PlayEnemyDamagePlayerMusic();

                animator.SetBool("Attack", true); // Normal enemy attack

                if (damageCoroutine == null)
                {
                    damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f)); // Damage the player with damageStrength
                }
            }


        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            animator.SetBool("Attack", false); // finish the animation of attack

            if (AnimatorHasParameter(animator, "BossAttack1"))
            {
                animator.SetBool("BossAttack1", false); // Set the method of attack to be method 1

                animator.SetBool("BossAttack2", false); // Set the method of attack to be method 2

                animator.SetBool("BossAttack3", false); // Set the method of attack to be method 3

            }

            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private bool AnimatorHasParameter(Animator animator, string paramName) // Check if the animator has the parameter
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
            {
                return true;
            }
        }
        return false;
    }




}
