using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum sourceName {PLAYER, FAKEMOCHI, MOCHI, GUARD1_BALCONY, GUARD2_BALCONY, GUARD1_ARREST, GUARD2_ARREST};
public class DialogueManager : MonoBehaviour
{
    public GameObject player;
    public GameObject fakeMochi; 
    public GameObject mochi;
    public GameObject guard1_balcony;
    public GameObject guard2_balcony;
    public GameObject guard1_arrest;
    public GameObject guard2_arrest;

    public AudioSource[] sources;

    void Awake()
    {
        sources = new AudioSource[7];
        sources[0] = player.GetComponent<AudioSource>();
        sources[1] = fakeMochi.GetComponent<AudioSource>();
        sources[2] = mochi.GetComponent<AudioSource>();
        sources[3] = guard1_balcony.GetComponent<AudioSource>();
        sources[4] = guard2_balcony.GetComponent<AudioSource>();
        sources[5] = guard1_arrest.GetComponent<AudioSource>();
        sources[6] = guard2_arrest.GetComponent<AudioSource>();
    }
}
