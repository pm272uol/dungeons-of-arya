using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    private AudioSource audioSource;


    private void Start()
    {
        gameObject.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        StartCoroutine(PlaySoundAndDisable());
    }

    IEnumerator PlaySoundAndDisable()
    {

        if (!isOpen)
        {
            audioSource.Play();
            // Wait for the sound to finish
            yield return new WaitForSeconds(audioSource.clip.length);

            isOpen = true;
            gameObject.SetActive(false); // Hide the door (or trigger an animation)
        }

    }
}
