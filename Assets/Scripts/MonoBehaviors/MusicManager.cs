using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;          // Main AudioSource
    public AudioClip backgroundMusic;         // Background music
    public AudioClip battleMusic;             // Battle music


    private bool isInBattle = false;          // Indicates whether in battle

    public AudioClip damageSound;

    public AudioClip EnemyDamagePlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackgroundMusic(); // Start playing the background music
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
}
