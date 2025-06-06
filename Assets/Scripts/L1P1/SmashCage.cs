using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class SmashCage : MonoBehaviour
{
    #region Variables

    public int hitsToDestroy = 5;
    public float forceThreshold = 10f;



    #endregion

    public UnityEvent event1;
    public GameObject realMochi;
    private GameObject panel;

    [Header("Audio Stuff")]
    AudioSource audioSource;
    public AudioClip hitSound;
    public PlaySoundRanTImes guardSleepSound;
    public DialogueScene dialogueScene;
    public DialogueScene cageBreakDialogueScene;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        event1 = new UnityEvent();

        event1.AddListener(WakeGameObject);
        realMochi.SetActive(false); 

        panel = GameObject.Find("PanelPlayer");
    }


    // ### Component Functions ###
    private void OnCollisionEnter(Collision collision)
    {
        // add pitch later
        if(collision.gameObject.CompareTag("Wall"))
        {
            float impactForce = collision.relativeVelocity.magnitude;
            audioSource.pitch = UnityEngine.Random.Range(1f, 1.5f);
            audioSource.PlayOneShot(hitSound);

            if (impactForce > forceThreshold)
            {
                hitsToDestroy--;
            }

            if (hitsToDestroy <= 0)
            {
                event1.Invoke();
                event1.RemoveListener(WakeGameObject);
                if (dialogueScene != null) { dialogueScene.keepPlaying = false; }
                else { Debug.Log("No Dialogue Scene"); }

                guardSleepSound.StartAudio();

                Destroy(gameObject);
                cageBreakDialogueScene.PlayAudio();
            }
        }
    }


    private void WakeGameObject()
    {
        realMochi.SetActive(true);
    }

}
