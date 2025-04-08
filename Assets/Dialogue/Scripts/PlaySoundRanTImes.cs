using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundRanTImes : MonoBehaviour
{
    public bool playing = true;
    public int maxTime = 5;
    public int randomTimeInterval;
    
    public RandAudioPlay AudioList;

    private System.Random rng;


    void Start()
    {
        rng = new System.Random(); // Initialize the random number generator
        randomTimeInterval = rng.Next(0, maxTime);
    }

    public void StartAudio()
    {
        StartCoroutine(RandTimePlay());
    }

    public void StopAudio() { playing = false; }

    IEnumerator RandTimePlay()
    {
        while(playing)
        {
            yield return new WaitForSeconds(randomTimeInterval);

            AudioList.PlayRandClip();
            yield return new WaitForSeconds(AudioList.clip.length);

            randomTimeInterval = rng.Next(0, maxTime);
        }
    }
}
