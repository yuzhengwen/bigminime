using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzuValen.Utils;

public class MusicHandler : MonoBehaviourSingletonPersistent<MusicHandler>
{
    public AudioClip music;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.Play();
    }
}
