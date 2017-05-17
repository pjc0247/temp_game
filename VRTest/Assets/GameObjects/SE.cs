using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour {
    public static SE instance;

    private AudioSource audioSource;

    public SE()
    {
        instance = this;
    }
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public static void Play(AudioClip audio)
    {
        if (audio == null)
            Debug.LogError("SE::Play - audio is nullptr");

        instance.audioSource.PlayOneShot(audio);
    }
}
