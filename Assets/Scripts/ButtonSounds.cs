using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource audioPlayer;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void HoverSound() {
        audioPlayer.PlayOneShot(hoverSound);
    }
    
    public void ClickSound() {
        audioPlayer.PlayOneShot(clickSound);        
    }
}
