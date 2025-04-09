using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlay : MonoBehaviour
{
    public List<AudioClip> Clips;
    public AudioSource source;

    public float clipVolume = 1f;

    private System.Random rng;

    void Awake()
    {
        rng = new System.Random(); 
    }

    public void PlayRandClip()
    {
        if (Clips.Count == 0) return;

        int clipNum = rng.Next(0, Clips.Count);
        AudioClip clip = Clips[clipNum];

        source.PlayOneShot(clip, clipVolume); // ✅ Volume controlled here
    }

}
