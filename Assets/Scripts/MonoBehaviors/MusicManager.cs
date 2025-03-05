using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    
    public AudioClip backgroundMusic;         // Background music
    public AudioClip battleMusic;             // Battle music

    public AudioClip Boss_Attack_1; // The sound of 3 boss attacks

    public AudioClip Boss_Attack_2;

    public AudioClip Boss_Attack_3;

    public AudioClip Boss_Death_Sound; // When the boss dies

    public AudioClip Enemy_Death_Sound;

    public AudioClip Player_Death_Sound;


    private bool isInBattle = false;          // Indicates whether in battle

    public AudioClip damageSound;

    public AudioClip EnemyDamagePlayer;

    public AudioSource audioSource;          // Main AudioSource

    public AudioSource audioSource2;         // Second AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackgroundMusic(); // Start playing the background music

        // SubScribe all Character onDeath Event
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (var enemy in enemies)
        {
            enemy.OnDeath += HandleEnemyDeath;
        }

        // Subscribe Player OnPlayerDeath Event
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            Debug.Log("Find Player");
            player.OnPlayerDeath += PlayPlayerDeath;
        }
    }

    //private void HandlePlayerDeath()
    //{
    //    PlayPlayerDeath(); // Play player death music
    //}



    private void HandleEnemyDeath(bool isBoss)
    {
        if (isBoss)
        {
            PlayBossDeath(); // Play Boss Death Sound
        }
        else
        {
            PlayEnemyDeath(); // Play Enemy Death Sound

        }
    }

    // Play the background music and set it to loop
    public void PlayBackgroundMusic()
    {
        if (audioSource.clip != backgroundMusic)
        {
            audioSource.clip = backgroundMusic; // Set to background music
            audioSource.loop = true;              // Set to loop
            audioSource.Play();                    // Play the background music
        }
    }

    // Play the battle music
    public void PlayBattleMusic()
    {
        
        if (!isInBattle)
        {
            Debug.Log("Play the battle music");

            isInBattle = true; // Mark as entering battle

            audioSource.Stop(); // Stop the current audio

            audioSource.clip = battleMusic; // Set to battle music
            audioSource.loop = true;         // Set to loop
            audioSource.Play();              // Play the battle music
        }
    }

    // Exit battle and play the background music
    public void StopBattleMusic()
    {
        
        if (isInBattle)
        {

            Debug.Log("Stop the battle music");

            isInBattle = false; // Mark as exiting battle

            audioSource.Stop(); // Stop the current audio

            
            PlayBackgroundMusic(); // Return to playing the background music
        }
    }

    public void PlayDamageMusic() // If anyone gets damaged
    {
        audioSource.PlayOneShot(damageSound);
    }

    public void PlayEnemyDamagePlayerMusic() // If anyone gets damaged
    {
        audioSource.PlayOneShot(EnemyDamagePlayer);
    }

    public void PlayBossAttack1()
    {
        audioSource2.PlayOneShot(Boss_Attack_1);
    }

    public void PlayBossAttack2()
    {
        audioSource2.PlayOneShot(Boss_Attack_2);
    }

    public void PlayBossAttack3()
    {
        audioSource2.PlayOneShot(Boss_Attack_3);
    }

    public void PlayBossDeath()
    {
        Debug.Log("A boss is dead");
        audioSource2.PlayOneShot(Boss_Death_Sound);
    }

    public void PlayEnemyDeath()
    {
        Debug.Log("An enemy is dead");
        audioSource2.PlayOneShot(Enemy_Death_Sound);
    }

    public void PlayPlayerDeath()
    {
        Debug.Log("Player is dead");
        audioSource2.PlayOneShot(Player_Death_Sound);
    }
}
