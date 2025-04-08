using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandAudioPlay : MonoBehaviour
{
    public List<AudioClip> Clips;
    public AudioClip clip;
    public AudioSource source;
    public string logWrite;

    private System.Random rng;

    void Awake()
    {
        rng = new System.Random(); // Initialize the random number generator
    }

    public void PlayRandClip()
    {
        if (Clips.Count == 0) return; // Prevent errors if list is empty

        int clipNum = rng.Next(0, Clips.Count); // Don't subtract 1; it's already exclusive
        clip = Clips[clipNum];

        Debug.Log(logWrite);
        source.PlayOneShot(clip);
    }
}
