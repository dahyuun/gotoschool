using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource Sound;

    public AudioClip clearSound;

    private void Start()
    {
        Sound = GetComponent<AudioSource>();
    }

    public void PlayEatSound()
    {
        Sound.PlayOneShot(clearSound);
    }
}
