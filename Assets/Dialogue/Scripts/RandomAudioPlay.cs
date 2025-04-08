using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlay : MonoBehaviour
{
    public List<AudioClip> Clips;
    public AudioSource source;

    private System.Random rng;

    void Awake()
    {
        rng = new System.Random(); 
    }

    public void PlayRandClip()
    {
        if (Clips.Count == 0) return; // Prevent errors if list is empty

        int clipNum = rng.Next(0, Clips.Count); 
        AudioClip clip = Clips[clipNum];

        source.PlayOneShot(clip);
    }
}
